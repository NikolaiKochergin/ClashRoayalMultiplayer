using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.EventSystems;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Source.Scripts.Infrastructure.Services.Input
{
    public class InputService : IInputService, IInitializable
    {
        private const string EventSystem = "EventSystem";

        private EventSystem _eventSystem;

        public async UniTask Initialize() => 
            await CreateEventSystem();

        private async UniTask CreateEventSystem()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(EventSystem);
            GameObject prefab = await handle;
            
            _eventSystem = Object.Instantiate(prefab).GetComponent<EventSystem>();
            Object.DontDestroyOnLoad(_eventSystem);
            
            Addressables.Release(handle);
        }
    }
}