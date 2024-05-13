using Reflex.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.Services.Enemy;
using Source.Scripts.GameCore.Battle.Services.Player;
using UnityEngine.SceneManagement;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class BattleState : IState
    {
        private readonly IPlayerService _player;
        private readonly IEnemyService _enemy;

        public BattleState(IPlayerService player, IEnemyService enemy)
        {
            _player = player;
            _enemy = enemy;
        }
        
        public void Enter()
        {
            MapInfo mapInfo = SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
            _player.Initialize(_enemy.Team, mapInfo.Player.Towers, mapInfo.Player.Units);
            _enemy.Initialize(_player.Team, mapInfo.Enemy.Towers, mapInfo.Enemy.Units);
        }

        public void Exit()
        {
        }
    }
}