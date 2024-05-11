using Reflex.Attributes;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.CommonElements
{
    public sealed class OpenWindowButton : ButtonBase
    {
        [SerializeField] private WindowId _windowId;
        
        private IWindowService _windows;

        [Inject]
        private void Construct(IWindowService windows) => 
            _windows = windows;

        protected override void OnButtonClicked() => 
            _windows.OpenWindow(_windowId);
    }
}