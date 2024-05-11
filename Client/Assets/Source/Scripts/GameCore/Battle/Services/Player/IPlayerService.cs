using System.Collections.Generic;
using Source.Scripts.GameCore.Battle.UnitLogic;

namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public interface IPlayerService
    {
        Team Team { get; }
        void Initialize(Team enemyTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<UnitBase> selfUnits);
    }
}