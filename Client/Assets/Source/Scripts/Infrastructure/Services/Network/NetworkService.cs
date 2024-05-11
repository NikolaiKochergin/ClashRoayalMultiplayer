using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.Networking;

namespace Source.Scripts.Infrastructure.Services.Network
{
    public class NetworkService : INetworkService
    {
        private const double Timout = 1;
        
        public async UniTaskVoid SendRequest(string uri, Dictionary<string, string> data, Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null)
        {
            using TimeoutController timeout = new TimeoutController();
            try
            {
                using UnityWebRequest webRequest = await UnityWebRequest
                    .Post(uri, data)
                    .SendWebRequest()
                    .WithCancellation(timeout.Timeout(TimeSpan.FromSeconds(Timout)));
                
                timeout.Reset();

                if (webRequest.result == UnityWebRequest.Result.Success)
                {
                    string result = DownloadHandlerBuffer.GetContent(webRequest);
                    onSuccessCallback?.Invoke(result);
                }
            }
            catch (OperationCanceledException)
            {
                if (timeout.IsTimeout())
                    onErrorCallback?.Invoke("Request timeout.");
            }
        }
    }
}