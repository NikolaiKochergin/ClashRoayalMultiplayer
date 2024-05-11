using System.Collections.Generic;
using Source.Scripts.GameCore.Battle.UnitLogic;

namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public class PlayerService : IPlayerService
    {
        public Team Team { get; } = new();

        public void Initialize(Team enemyTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<UnitBase> selfUnits)
        {
            foreach (Tower tower in selfTowers)
            {
                tower.Construct();
                Team.Add(tower);
            }

            foreach (UnitBase unit in selfUnits)
            {
                unit.Construct(enemyTeam);
                Team.Add(unit);
            }
        }
    }
}