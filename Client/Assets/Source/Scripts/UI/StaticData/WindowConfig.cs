using System;
using Source.Scripts.StaticData.References;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.StaticData
{
    [Serializable]
    public class WindowConfig
    {
        [field: SerializeField] public WindowId WindowId { get; private set; }
        [field: SerializeField] public ComponentReference<WindowBase> Reference { get; private set; }
    }
}