using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.Infrastructure.Services.Factory
{
    public interface IGameFactory
    {
        UniTask<T> Create<T>(AssetReference reference) where T : MonoBehaviour;
        UniTask<T> Create<T>(string address) where T : MonoBehaviour;
    }
}