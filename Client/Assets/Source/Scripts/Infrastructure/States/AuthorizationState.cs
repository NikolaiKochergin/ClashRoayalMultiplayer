using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class AuthorizationState : IState
    {
        private readonly IWindowService _windows;

        public AuthorizationState(IWindowService windows) => 
            _windows = windows;

        public void Enter()
        {
            _windows.OpenWindow(WindowId.Background);
            _windows.OpenWindow(WindowId.Authorization);
        }

        public void Exit() => 
            _windows.CloseWindow(WindowId.Authorization);
    }
}