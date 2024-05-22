using Source.Scripts.GameCore.Battle.Services.Enemy;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.Multiplayer;
using Source.Scripts.Multiplayer.Data;
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
        private readonly IMultiplayerService _multiplayer;

        public BattleState(
            IPlayerService player, 
            IEnemyService enemy, 
            IWindowService windows,
            IMultiplayerService multiplayer)
        {
            _player = player;
            _enemy = enemy;
            _windows = windows;
            _multiplayer = multiplayer;
        }
        
        public void Enter()
        {
            _player.Initialize(_enemy.Team);
            _enemy.Initialize(_player.Team);
            _windows.OpenWindow(WindowId.Countdown);
            _multiplayer.StartTickHappened += OnStartTickHappened;
        }

        public void Exit()
        {
            _multiplayer.StartTickHappened += OnStartTickHappened;
            _windows.CloseWindow(WindowId.Battle);
        }

        private void OnStartTickHappened(TickData data)
        {
            if (data.tick == 10)
            {
                _windows.CloseWindow(WindowId.Countdown);
                _windows.OpenWindow(WindowId.Battle);
            }
        }
    }
}