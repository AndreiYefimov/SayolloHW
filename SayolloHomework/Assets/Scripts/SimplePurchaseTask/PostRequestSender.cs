using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace AndriiYefimov.SayolloHW2
{
    public class PostRequestSender
    {
        [Serializable]
        protected class EmptyModel { }

        private readonly string _defaultPostData;

        public PostRequestSender()
        {
            _defaultPostData = JsonUtility.ToJson(new EmptyModel());
        }

        public IEnumerator SendSimplePostRequest(string requestUrl, string postData,
            Action responseCallback, Action<string> errorCallback = null)
        {
            var uwr = SetupRequest(requestUrl, postData);

            yield return uwr.SendWebRequest();

            var hasErrors = uwr.isNetworkError || uwr.isHttpError;
            TryHandleErrors(hasErrors, uwr, errorCallback);
            TryHandleResult(responseCallback, hasErrors, uwr);
        }
        
        public IEnumerator SendPostRequest<T>(string requestUrl,
            Action<T> responseCallback, Action<string> errorCallback = null)
        {
            yield return GetRequestRoutine(requestUrl, _defaultPostData, responseCallback, errorCallback);
        }

        public IEnumerator SendPostRequest<T>(string requestUrl, string postData,
            Action<T> responseCallback, Action<string> errorCallback = null)
        {
            yield return GetRequestRoutine(requestUrl, postData, responseCallback, errorCallback);
        }

        private IEnumerator GetRequestRoutine<T>(string requestUrl, string postData,
            Action<T> responseCallback, Action<string> errorCallback = null)
        {
            var uwr = SetupRequest(requestUrl, postData);

            yield return uwr.SendWebRequest();

            var hasErrors = uwr.isNetworkError || uwr.isHttpError;
            TryHandleErrors(hasErrors, uwr, errorCallback);
            TryHandleResult(responseCallback, hasErrors, uwr);
        }

        private static void TryHandleErrors(bool hasErrors, UnityWebRequest uwr, Action<string> errorCallback)
        {
            if (hasErrors)
            {
                Debug.LogError($"Error: {uwr.error}");
                errorCallback?.Invoke(uwr.error);
            }
        }

        private static void TryHandleResult<T>(Action<T> responseCallback, bool hasErrors, UnityWebRequest uwr)
        {
            if (!hasErrors && responseCallback != null)
            {
                Debug.Log($"Result: {uwr.downloadHandler.text}");
                var response = JsonUtility.FromJson<T>(uwr.downloadHandler.text);
                responseCallback.Invoke(response);
            }
        }
        
        private static void TryHandleResult(Action responseCallback, bool hasErrors, UnityWebRequest uwr)
        {
            if (!hasErrors && responseCallback != null)
            {
                Debug.Log($"Result: {uwr.downloadHandler.text}");
                responseCallback.Invoke();
            }
        }

        private static UnityWebRequest SetupRequest(string requestUrl, string postData)
        {
            var uwr = new UnityWebRequest(requestUrl, "POST");
            SetDefaultRequestProperties(postData, uwr);
            return uwr;
        }

        private static void SetDefaultRequestProperties(string postData, UnityWebRequest uwr)
        {
            var jsonToSend = new UTF8Encoding().GetBytes(postData);
            uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
            uwr.downloadHandler = new DownloadHandlerBuffer();
            uwr.SetRequestHeader("Content-Type", "application/json");
        }
    }
}