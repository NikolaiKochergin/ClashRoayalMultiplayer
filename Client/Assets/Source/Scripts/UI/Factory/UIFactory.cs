using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Reflex.Injectors;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Object = UnityEngine.Object;

namespace Source.Scripts.UI.Factory
{
    public class UIFactory : IUIFactory, IInitializable
    {
        private const string UIRootName = "UIRoot";
        private const string UIConfig = "UIConfig";

        private readonly Container _container;
        
        private IReadOnlyDictionary<WindowId, WindowBase> _windowsMap;

        public UIFactory(Container container) => 
            _container = container;

        public UIRoot UIRoot { get; private set; }

        public async UniTask Initialize()
        {
            await LoadWindowsConfig();
            await CreateUIRoot();
        }

        public WindowBase CreateWindow(WindowId id)
        {
            if(_windowsMap.TryGetValue(id, out WindowBase prefab) == false || prefab == null)
                throw new ArgumentException($"Window Config with id: {id} not found or window prefab is null.");
                
            return InstantiateAndInject(prefab, UIRoot.transform);
        }

        public T Create<T>(T uiElement, Transform parent) where T : Component => 
            InstantiateAndInject(uiElement, parent);

        private T InstantiateAndInject<T>(T prefab, Transform parent = null) where T : Component
        {
            T instance = Object.Instantiate(prefab, parent);
            GameObjectInjector.InjectRecursive(instance.gameObject, _container);

            return instance;
        }

        private async UniTask LoadWindowsConfig() => 
            _windowsMap = (await Addressables.LoadAssetAsync<UIConfig>(UIConfig)).WindowsMap;

        private async UniTask CreateUIRoot()
        {
            AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(UIRootName);
            GameObject uiRootPrefab = await handle;
            
            UIRoot = Object.Instantiate(uiRootPrefab).GetComponent<UIRoot>();
            UIRoot.SetCanvas(Camera.main);
            Object.DontDestroyOnLoad(UIRoot);
            
            Addressables.Release(handle);
        }
    }
}