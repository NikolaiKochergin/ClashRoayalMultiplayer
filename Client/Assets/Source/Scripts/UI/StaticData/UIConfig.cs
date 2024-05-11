using System.Collections.Generic;
using System.Linq;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.StaticData
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Static Data/UI Config", order = 0)]
    public class UIConfig : ScriptableObject
    {
        [SerializeField] private List<WindowConfig> _windowsConfig;

        public IReadOnlyDictionary<WindowId, WindowBase> WindowsMap => 
            _windowsConfig.ToDictionary(config => config.WindowId, config => config.Prefab);

#if UNITY_EDITOR
        private void OnValidate()
        {
            IReadOnlyDictionary<WindowId, WindowBase> map = WindowsMap;
            if(map.ContainsKey(WindowId.Unknown))
                Debug.LogError($"Windows map contains key: {WindowId.Unknown}");
        }
#endif
    }
}