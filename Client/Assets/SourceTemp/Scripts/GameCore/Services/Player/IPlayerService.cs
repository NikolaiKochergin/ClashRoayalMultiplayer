using System.Collections.Generic;
using Source.Scripts.GameCore.UnitLogic;

namespace Source.Scripts.GameCore.Services.Player
{
    public interface IPlayerService
    {
        Team Team { get; }
        void Initialize(Team enemyTeam, IReadOnlyList<Tower> selfTowers, IReadOnlyList<UnitBase> selfUnits);
    }
}