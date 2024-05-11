using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.Services.Enemy
{
    public interface IEnemyService
    {
        Team Team { get; }
        void Initialize(Team playerTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<UnitBase> selfUnits);
    }
}