using System;
using System.IO;
using AndriiYefimov.SayolloHW.Loaders;
using AndriiYefimov.SayolloHW.Models;
using AndriiYefimov.SayolloHW.Models.XMLModels;
using AndriiYefimov.SayolloHW.Savers;
using AndriiYefimov.SayolloHW.Senders;
using UnityEngine;
using UnityEngine.UI;

namespace AndriiYefimov.SayolloHW.Test
{
    public class TestAdsManager : MonoBehaviour
    {
        [Header("View Components")]
        [SerializeField] private Button playVideoButton;
        [SerializeField] private Canvas canvas;

        [Header("Components")] 
        [SerializeField] private VideoController videoController;
        
        [Header("Test api url")]
        [SerializeField] private string apiUrl = "https://6u3td6zfza.execute-api.us-east-2.amazonaws.com/prod/ad/vast";

        [Header("Video File configuration")]
        [SerializeField] private string videoDefaultName = "myVideo.mp4";  /* TEST FIELD */
        [SerializeField] private string cachedVideosPath = "CacheContent/Videos";  /* TEST FIELD */
        
        private string _videoLink = "";  /* TEST FIELD */
        private FileSaver _fileSaver;
        private FileLoader _fileLoader;
        private GetRequestSender _getRequestSender;
        private VideoFileModel _testVideoFileModel; /* TEST FIELD */

        private void Awake()
        {
            InitComponents();
        }
        
        private void OnEnable()
        {
            AddListeners();
        }

        private void OnDisable()
        {
            RemoveListeners();
        }
        
        private void InitComponents()
        {
            _fileSaver = new FileSaver();
            _fileLoader = new FileLoader();
            _getRequestSender = new GetRequestSender();
        }

        private void AddListeners()
        {
            playVideoButton.onClick.AddListener(PlayAdVideo);
        }

        private void RemoveListeners()
        {
            playVideoButton.onClick.RemoveListener(PlayAdVideo);
        }
        
        private void PlayAdVideo()
        {
            StartCoroutine(_getRequestSender.SendXMLGetRequest<VAST>(apiUrl, OnGetRequestResponse));
        }

        private void OnGetRequestResponse(VAST response)
        {
            HandleResponse(response);

            var isExist = File.Exists(_testVideoFileModel.FileFullPath);
            TryLoadFileAndPlay(isExist);
            TryInitPlayingVideo(isExist);
        }

        private void TryInitPlayingVideo(bool isExist)
        {
            if (isExist) 
                InitVideoPlaying();
        }

        private void TryLoadFileAndPlay(bool isExist)
        {
            if (isExist) return;
            
            _fileLoader.FileLoadStateChanged += OnFileLoaded;
            StartCoroutine(_fileLoader.DownloadFile(_videoLink));
        }

        private void OnFileLoaded(FileLoadState loadState, byte[] fileData)
        {
            if (loadState != FileLoadState.Finished) 
                return;
            
            _fileLoader.FileLoadStateChanged -= OnFileLoaded;
            
            _fileSaver.SaveToDisk(_testVideoFileModel, fileData);
            InitVideoPlaying();
        }

        private void InitVideoPlaying()
        {
            videoController.VideoPlayingStateChanged += OnVideoPlayingStarted;
            videoController.VideoPlayingStateChanged += OnVideoPlayingFinished;
            videoController.PlayVideo(_testVideoFileModel);
        }
        
        private void HandleResponse(VAST response)
        {
            var durationString = response.Ad.InLine.Creatives.Creative.Linear.Duration;
            TimeSpan.TryParse(durationString, out var duration);
            Debug.Log($"<b>parsedVast.*.Duration:</b> {duration.Seconds}");

            _videoLink = response.Ad.InLine.Creatives.Creative.Linear.MediaFiles.MediaFile;
            Debug.Log($"<b>parsedVast.*.MediaFile:</b> {_videoLink}");
            
            _testVideoFileModel = new VideoFileModel(
                $"{Application.persistentDataPath}/{cachedVideosPath}", videoDefaultName, duration);
        }

        private void OnVideoPlayingStarted(VideoPlayingState playingState)
        {
            OnVideoPlayingStateChanged(OnVideoPlayingStarted,
                VideoPlayingState.Started, playingState, false);
        }
        
        private void OnVideoPlayingFinished(VideoPlayingState playingState)
        {
            OnVideoPlayingStateChanged(OnVideoPlayingFinished,
                VideoPlayingState.Finished, playingState, true);
        }

        private void OnVideoPlayingStateChanged(Action<VideoPlayingState> removalCallback,
            VideoPlayingState requiredState, VideoPlayingState current, bool canvasActiveState)
        {
            if (current != requiredState) return;
            
            videoController.VideoPlayingStateChanged -= removalCallback;
            SetCanvasActiveState(canvasActiveState);
        }

        private void SetCanvasActiveState(bool state)
        {
            canvas.gameObject.SetActive(state);
        }
    }
}