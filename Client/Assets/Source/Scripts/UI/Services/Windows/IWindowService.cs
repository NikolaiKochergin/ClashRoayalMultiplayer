using Cysharp.Threading.Tasks;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.UI.Services.Windows
{
    public interface IWindowService
    {
        UniTask OpenWindow(WindowId id);
        void CloseWindow(WindowId id);
        void CloseAll();
    }
}