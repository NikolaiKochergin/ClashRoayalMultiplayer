using System.Collections.Generic;
using Source.Scripts.GameCore.Battle.UnitLogic;

namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public interface IEnemyService
    {
        Team Team { get; }
        void Initialize(Team playerTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<UnitBase> selfUnits);
    }
}