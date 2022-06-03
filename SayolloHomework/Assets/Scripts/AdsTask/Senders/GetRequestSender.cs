using System;
using System.Collections;
using AndriiYefimov.SayolloHW.Extentions;
using AndriiYefimov.SayolloHW2.Extentions;
using UnityEngine;
using UnityEngine.Networking;

namespace AndriiYefimov.SayolloHW.Senders
{
    public class GetRequestSender
    {

        public IEnumerator SendImageGetRequest(string url, Action<Sprite> resultCallback)
        {
            var www = UnityWebRequest.Get(url);

            yield return www.SendWebRequest();

            HandleImageGetRequest(resultCallback, www);
        }

        public IEnumerator SendXMLGetRequest<T>(string url, Action<T> resultCallback) where T : class
        {
            var www = UnityWebRequest.Get(url);

            yield return www.SendWebRequest();

            HandleXmlGetRequest(resultCallback, www);
        }
        
        private void HandleImageGetRequest(Action<Sprite> resultCallback, UnityWebRequest www)
        {
            var hasError = www.isNetworkError || www.isHttpError;

            TryHandleError(hasError, www);
            var result = TryHandleByteResult(hasError, www);
            
            resultCallback?.Invoke(result.ToSprite());
        }

        private void HandleXmlGetRequest<T>(Action<T> resultCallback, UnityWebRequest www, string result = "")
            where T : class
        {
            var hasError = www.isNetworkError || www.isHttpError;
            
            TryHandleError(hasError, www);
            result = TryHandleTextResult(hasError, www, result);

            resultCallback?.Invoke(result.ParseXML<T>());
        }

        private string TryHandleTextResult(bool hasError, UnityWebRequest www, string result)
        {
            if (hasError) return result;

            Debug.Log("Form upload complete!");
            Debug.Log($"<b>result[1]:</b> <i>*see the next line*</i>\n{www.downloadHandler.text}");

            return www.downloadHandler.text;
        }
        
        private byte[] TryHandleByteResult(bool hasError, UnityWebRequest www)
        {
            if (hasError) return default;

            Debug.Log("Form upload complete! Received byte data.");

            return www.downloadHandler.data;
        }

        private static void TryHandleError(bool hasError, UnityWebRequest www)
        {
            if (hasError)
            {
                Debug.LogError(www.error);
            }
        }
    }
}