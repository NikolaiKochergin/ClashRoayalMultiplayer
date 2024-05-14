using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.StaticData.References
{
    [Serializable]
    public class ComponentReference<TComponent> : AssetReference
    {
        public ComponentReference(string guid) : base(guid) {}
        
        public override bool ValidateAsset(string path)
        {
#if UNITY_EDITOR
            GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            return go != null && go.GetComponent<TComponent>() != null;
#else
            return false;
#endif
        }
    }
}