using Reflex.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.UnitLogic;
using UnityEngine.SceneManagement;

namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        private readonly Team _team = new();
        private IReadOnlyTeam _playerTeam;
        public IReadOnlyTeam Team => _team;

        public void Initialize(IReadOnlyTeam playerTeam)
        {
            _playerTeam = playerTeam;
            MapInfo map = GetMapInfo();
            
            foreach (Tower tower in map.Enemy.Towers)
            {
                tower.Construct();
                _team.Add(tower);
            }

            foreach (UnitBase unit in map.Enemy.Units)
            {
                unit.Construct(playerTeam);
                _team.Add(unit);
            } 
        }
        
        private static MapInfo GetMapInfo() => 
            SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
    }
}