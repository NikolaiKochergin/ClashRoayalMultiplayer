using System;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        [field: SerializeField] public WindowId WindowId { get; private set; }
        [field: SerializeField] public WindowBase Prefab { get; private set; }
    }
}