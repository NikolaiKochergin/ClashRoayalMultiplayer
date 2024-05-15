using Reflex.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.Services.Enemy;
using Source.Scripts.GameCore.Battle.UnitLogic;
using UnityEngine.SceneManagement;

namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly IEnemyService _enemy;
        public Team Team { get; } = new();

        public PlayerService(IEnemyService enemy)
        {
            _enemy = enemy;
        }

        public void Initialize()
        {
            MapInfo map = GetMapInfo();
            
            foreach (Tower tower in map.Player.Towers)
            {
                tower.Construct();
                Team.Add(tower);
            }

            foreach (UnitBase unit in map.Player.Units)
            {
                unit.Construct(_enemy.Team);
                Team.Add(unit);
            }
        }

        private static MapInfo GetMapInfo() => 
            SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
    }
}