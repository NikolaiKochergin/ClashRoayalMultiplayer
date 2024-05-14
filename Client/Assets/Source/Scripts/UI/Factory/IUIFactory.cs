using Cysharp.Threading.Tasks;
using Source.Scripts.UI.Windows;
using UnityEngine;

namespace Source.Scripts.UI.Factory
{
    public interface IUIFactory
    {
        UniTask<WindowBase> CreateWindow(WindowId id);
        T Create<T>(T uiElement, Transform container) where T : Component;
        UIRoot UIRoot { get; }
    }
}