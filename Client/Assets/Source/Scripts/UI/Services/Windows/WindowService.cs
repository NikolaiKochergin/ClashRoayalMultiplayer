using System.Collections.Generic;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _factory;
        private readonly Dictionary<WindowId, WindowBase> _openedWindows = new Dictionary<WindowId, WindowBase>();

        public WindowService(IUIFactory factory) => 
            _factory = factory;

        public void OpenWindow(WindowId id)
        {
            if (_openedWindows.ContainsKey(id))
                return;
            
            _openedWindows.Add(id, _factory.CreateWindow(id));
        }

        public void CloseWindow(WindowId id)
        {
            if(_openedWindows.Remove(id, out WindowBase window) && window != null)
                window.Close();
        }

        public void Cleanup()
        {
            foreach (WindowId id in _openedWindows.Keys) 
                CloseWindow(id);
        }
    }
}