using System;
using UnityEngine.AddressableAssets;
using Object = UnityEngine.Object;

namespace Source.Scripts.StaticData.References
{
    [Serializable]
    public class AssetReferenceScene : AssetReference
    {
        public AssetReferenceScene(string guid) : base(guid) { }

        public override bool ValidateAsset(Object obj)
        {
#if UNITY_EDITOR
            Type type = obj.GetType();
            return typeof(UnityEditor.SceneAsset).IsAssignableFrom(type);
#else
            return false;
#endif
        }

        public override bool ValidateAsset(string path)
        {
#if UNITY_EDITOR
            Type type = UnityEditor.AssetDatabase.GetMainAssetTypeAtPath(path);
            return typeof(UnityEditor.SceneAsset).IsAssignableFrom(type);
#else
            return false;
#endif
        }
    }
}