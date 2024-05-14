using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.Infrastructure.Services.AssetManagement
{
    public interface IAsset
    {
        UniTask<T> Load<T>(AssetReference assetReference) where T : Object;
        UniTask<T> Load<T>(string address) where T : Object;
    }
}