using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Source.Scripts.Infrastructure
{
    public class SceneLoader
    {
        private static AsyncOperationHandle<SceneInstance> _handle;

        public float Progress => _handle.PercentComplete;

        public async UniTask LoadAsync(string nextScene)
        {
            _handle =  Addressables.LoadSceneAsync(nextScene);
            await UniTask.WaitUntil(() => _handle.Status == AsyncOperationStatus.Succeeded);
        }
    }
}