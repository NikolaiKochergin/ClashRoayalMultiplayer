using System.Collections.Generic;
using Reflex.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.GameCore.Battle.UnitLogic;
using UnityEngine.SceneManagement;

namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        private readonly IPlayerService _player;
        public Team Team { get; } = new();

        // public EnemyService(IPlayerService player)
        // {
        //     _player = player;
        // }

        public void Initialize()
        {
            MapInfo map = GetMapInfo();
            
            foreach (Tower tower in map.Enemy.Towers)
            {
                tower.Construct();
                Team.Add(tower);
            }

            foreach (UnitBase unit in map.Enemy.Units)
            {
                unit.Construct(_player.Team);
                Team.Add(unit);
            } 
        }
        
        private static MapInfo GetMapInfo() => 
            SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
    }
}