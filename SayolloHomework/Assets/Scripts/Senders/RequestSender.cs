using System;
using System.Collections;
using AndriiYefimov.SayolloHW.Extentions;
using UnityEngine;
using UnityEngine.Networking;

namespace AndriiYefimov.SayolloHW.Senders
{
    public class RequestSender
    {
        public IEnumerator SendGetRequest<T>(string url, Action<T> resultCallback) where T : class
        {
            var www = UnityWebRequest.Get(url);

            yield return www.SendWebRequest();

            HandleGetRequest(resultCallback, www);
        }

        private void HandleGetRequest<T>(Action<T> resultCallback, UnityWebRequest www, string result = "")
            where T : class
        {
            var hasError = www.isNetworkError || www.isHttpError;

            TryHandleError<T>(hasError, www);
            result = TryHandleResult<T>(hasError, www, result);

            resultCallback?.Invoke(result.ParseXML<T>());
        }

        private string TryHandleResult<T>(bool hasError, UnityWebRequest www, string result) where T : class
        {
            if (hasError) return result;

            Debug.Log("Form upload complete!");
            Debug.Log($"<b>result[1]:</b> <i>*see the next line*</i>\n{www.downloadHandler.text}");

            return www.downloadHandler.text;
        }

        private static void TryHandleError<T>(bool hasError, UnityWebRequest www) where T : class
        {
            if (hasError)
            {
                Debug.LogError(www.error);
            }
        }
    }
}