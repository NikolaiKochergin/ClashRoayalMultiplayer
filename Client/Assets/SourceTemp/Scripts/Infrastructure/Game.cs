using Source.Scripts.GameCore.MapLogic;
using Source.Scripts.GameCore.Services.Enemy;
using Source.Scripts.GameCore.Services.Player;

namespace Source.Scripts.Infrastructure
{
    public class Game
    {
        private readonly IPlayerService _player;
        private readonly IEnemyService _enemy;
        private readonly MapInfo _mapInfo;

        public Game(
            IPlayerService player, 
            IEnemyService enemy,
            MapInfo mapInfo)
        {
            _player = player;
            _enemy = enemy;
            _mapInfo = mapInfo;
        }

        public void Start()
        {
            _player.Initialize(_enemy.Team, _mapInfo.Player.Towers, _mapInfo.Player.Units);
            _enemy.Initialize(_player.Team, _mapInfo.Enemy.Towers, _mapInfo.Enemy.Units);
        }
    }
}