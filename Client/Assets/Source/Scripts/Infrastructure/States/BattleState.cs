using Reflex.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.Services.Enemy;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine.SceneManagement;
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
            MapInfo mapInfo = SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
            _player.Initialize(_enemy.Team, mapInfo.Player.Towers, mapInfo.Player.Units);
            _enemy.Initialize(_player.Team, mapInfo.Enemy.Towers, mapInfo.Enemy.Units);
            _windows.OpenWindow(WindowId.Battle);
        }

        public void Exit()
        {
            _windows.CloseWindow(WindowId.Battle);
        }
    }
}