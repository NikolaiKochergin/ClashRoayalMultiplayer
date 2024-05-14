using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Source.Scripts.Infrastructure.Services.AssetManagement
{
    public class Asset : IAsset, IInitializable, ICleanable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _completedCache = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();
        
        public async UniTask Initialize() => 
            await Addressables.InitializeAsync();

        public async UniTask<T> Load<T>(AssetReference assetReference) where T : Object
        {
            if (_completedCache.TryGetValue(assetReference.AssetGUID, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(assetReference),
                cacheKey: assetReference.AssetGUID);
        }

        public async UniTask<T> Load<T>(string address) where T : Object
        {
            if (_completedCache.TryGetValue(address, out AsyncOperationHandle completedHandle))
                return completedHandle.Result as T;

            return await RunWithCacheOnComplete(
                Addressables.LoadAssetAsync<T>(address),
                cacheKey: address);
        }

        public void Cleanup()
        {
            foreach (List<AsyncOperationHandle> resourceHandles in _handles.Values)
            foreach (AsyncOperationHandle handle in resourceHandles) 
                    Addressables.Release(handle);
            
            _completedCache.Clear();
            _handles.Clear();
        }

        private async UniTask<T> RunWithCacheOnComplete<T>(AsyncOperationHandle<T> handle, string cacheKey) where T : class
        {
            handle.Completed += completeHandle => _completedCache[cacheKey] = completeHandle;

            AddHandle<T>(cacheKey, handle);

            return await handle.Task.AsUniTask();
        }

        private void AddHandle<T>(string key, AsyncOperationHandle handle) where T : class
        {
            if (!_handles.TryGetValue(key, out List<AsyncOperationHandle> resourceHandles))
            {
                resourceHandles = new List<AsyncOperationHandle>();
                _handles[key] = resourceHandles;
            }
            
            resourceHandles.Add(handle);
        }
    }
}