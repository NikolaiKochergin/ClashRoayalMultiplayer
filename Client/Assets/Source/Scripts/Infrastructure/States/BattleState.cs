using Source.Scripts.GameCore.Battle.Services.Enemy;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class BattleState : IState
    {
        private readonly IPlayerService _player;
        private readonly IEnemyService _enemy;
        private readonly IWindowService _windows;

        public BattleState(IPlayerService player, IEnemyService enemy, IWindowService windows)
        {
            _player = player;
            _enemy = enemy;
            _windows = windows;
        }
        
        public void Enter()
        {
            _player.Initialize();
            _enemy.Initialize();
            _windows.OpenWindow(WindowId.Battle);
        }

        public void Exit()
        {
            _windows.CloseWindow(WindowId.Battle);
        }
    }
}