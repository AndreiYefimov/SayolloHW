using System;
using System.Collections;
using AndriiYefimov.SayolloHW.Models;
using AndriiYefimov.SayolloHW.TimeManagement;
using UnityEngine;
using UnityEngine.Video;

namespace AndriiYefimov.SayolloHW
{
    public class VideoController : MonoBehaviour
    {
        [SerializeField] private VideoPlayer videoPlayer;

        private WaitForSeconds _waitForOneSecond;
        private Timer _timer;

        public event Action<VideoPlayingState> VideoPlayingStateChanged;

        private void Awake()
        {
            InitComponents();
        }

        private void InitComponents()
        {
            _timer = new Timer();
            _waitForOneSecond = new WaitForSeconds(1);
        }

        public void PlayVideo(VideoFileModel fileModel)
        {
            StartCoroutine(HandlePlayVideoRequest(fileModel.FileFullPath, fileModel.Duration));
        }

        private IEnumerator HandlePlayVideoRequest(string path, float duration)
        {
            yield return WaitForVideoPrepared(path);
            
            SetupTimer(duration);
            InitVideoPlaying();
        }

        private void InitVideoPlaying()
        {
            VideoPlayingStateChanged?.Invoke(VideoPlayingState.Started);
            UpdatePlayerCameraState(true);
            videoPlayer.Play();
        }

        private void SetupTimer(float duration)
        {
            _timer.TimerStateChanged += OnTimerFinished;
            StartCoroutine(_timer.StartTimer(duration));
        }

        private void OnTimerFinished(TimerState state)
        {
            if (state != TimerState.Finished) return;
            
            _timer.TimerStateChanged -= OnTimerFinished;
            UpdatePlayerCameraState(false);
            VideoPlayingStateChanged?.Invoke(VideoPlayingState.Finished);
        }

        private void UpdatePlayerCameraState(bool state)
        {
            videoPlayer.targetCamera.enabled = state;
        }

        private IEnumerator WaitForVideoPrepared(string path)
        {
            videoPlayer.url = path;
            videoPlayer.Prepare();

            while (!videoPlayer.isPrepared)
            {
                yield return _waitForOneSecond;
                break;
            }
        }
    }
}