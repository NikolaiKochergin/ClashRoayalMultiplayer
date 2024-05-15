using Cysharp.Threading.Tasks;
using Reflex.Core;
using Reflex.Injectors;
using Source.Scripts.Infrastructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.Infrastructure.Services.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly Container _container;
        private readonly IAsset _asset;

        public GameFactory(Container container, IAsset asset)
        {
            _container = container;
            _asset = asset;
        }

        public async UniTask<T> Create<T>(AssetReference reference) where T : MonoBehaviour
        {
            GameObject prefab = await _asset.Load<GameObject>(reference);
            return InstantiateAndInject<T>(prefab);
        }
        
        public async UniTask<T> Create<T>(string address) where T : MonoBehaviour
        {
            GameObject prefab = await _asset.Load<GameObject>(address);
            return InstantiateAndInject<T>(prefab);
        }

        private T InstantiateAndInject<T>(GameObject prefab) where T : MonoBehaviour
        {
            GameObject newObject = Object.Instantiate(prefab);
            GameObjectInjector.InjectObject(newObject, _container);
            return newObject.GetComponent<T>();
        }
    }
}