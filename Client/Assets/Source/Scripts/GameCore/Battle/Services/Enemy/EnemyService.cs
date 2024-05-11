using System.Collections.Generic;
using Source.Scripts.GameCore.Battle.UnitLogic;

namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        public Team Team { get; } = new();

        public void Initialize(Team playerTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<UnitBase> selfUnits)
        {
            foreach (Tower tower in selfTowers)
            {
                tower.Construct();
                Team.Add(tower);
            }

            foreach (UnitBase unit in selfUnits)
            {
                unit.Construct(playerTeam);
                Team.Add(unit);
            } 
        }
    }
}