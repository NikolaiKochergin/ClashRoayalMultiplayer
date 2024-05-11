using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.Infrastructure.Services.Network
{
    public interface INetworkService
    {
        UniTaskVoid SendRequest(string uri, Dictionary<string, string> data, Action<string> onSuccessCallback = null, Action<string> onErrorCallback = null);
    }
}