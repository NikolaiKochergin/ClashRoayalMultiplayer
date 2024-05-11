using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.Infrastructure.States
{
    public class AuthorizationState : IState
    {
        private readonly IWindowService _windows;

        public AuthorizationState(IWindowService windows) => 
            _windows = windows;

        public void Enter() => 
            _windows.OpenWindow(WindowId.Authorization);

        public void Exit() => 
            _windows.CloseWindow(WindowId.Authorization);
    }
}