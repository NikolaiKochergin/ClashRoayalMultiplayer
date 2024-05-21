using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.UI.Services.Windows
{
    public class WindowService : IWindowService
    {
        private readonly IUIFactory _factory;
        private readonly Dictionary<WindowId, WindowBase> _openedWindows = new();

        public WindowService(IUIFactory factory) => 
            _factory = factory;

        public async UniTask OpenWindow(WindowId id)
        {
            if (_openedWindows.TryAdd(id, null))
                _openedWindows[id] = await _factory.CreateWindow(id);
        }

        public void CloseWindow(WindowId id)
        {
            if(_openedWindows.Remove(id, out WindowBase window))
                window.Close();
        }

        public void CloseAll()
        {
            List<WindowId> ids = _openedWindows.Keys.ToList();
            foreach (WindowId id in ids) 
                CloseWindow(id);
        }
    }
}