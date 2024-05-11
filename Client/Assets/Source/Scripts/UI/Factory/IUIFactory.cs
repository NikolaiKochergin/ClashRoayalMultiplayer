using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.Factory
{
    public interface IUIFactory
    {
        WindowBase CreateWindow(WindowId id);
        T Create<T>(T uiElement, Transform container) where T : Component;
        UIRoot UIRoot { get; }
    }
}