using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace AndriiYefimov.SayolloHW.Loaders
{
    public class FileLoader
    {
        public event Action<FileLoadState, byte[]> FileLoadStateChanged;
        
        public IEnumerator DownloadFile(string videoUrl)
        {
            var webRequest = UnityWebRequest.Get(videoUrl);
            
            FileLoadStateChanged?.Invoke(FileLoadState.Started, null);
            webRequest.SendWebRequest();

            yield return HandleRequestResponse(webRequest);
        }
        
        private IEnumerator HandleRequestResponse(UnityWebRequest request)
        {
            if (TryHandleError(request)) yield break;
            yield return TryHandleLoadingResult(request);

            FileLoadStateChanged?.Invoke(FileLoadState.Finished, request.downloadHandler.data);
        }

        private IEnumerator TryHandleLoadingResult(UnityWebRequest request)
        {
            while (!request.isDone)
            {
                Debug.Log($"Process: {request.downloadProgress}");
                yield return null;
            }
        }

        private bool TryHandleError(UnityWebRequest request)
        {
            if (!request.isNetworkError && !request.isHttpError) 
                return false;
            
            Debug.Log($"Error: {request.error}");
            return true;
        }
    }
}