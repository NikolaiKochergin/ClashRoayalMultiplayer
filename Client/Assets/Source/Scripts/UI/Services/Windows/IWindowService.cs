using Source.Scripts.UI.StaticData;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.UI.Services.Windows
{
    public interface IWindowService
    {
        void OpenWindow(WindowId id);
        void CloseWindow(WindowId id);
        void Cleanup();
    }
}