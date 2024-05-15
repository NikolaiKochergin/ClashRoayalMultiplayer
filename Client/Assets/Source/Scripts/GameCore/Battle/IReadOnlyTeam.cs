using System.Collections.Generic;
using Source.Scripts.GameCore.Battle.UnitLogic;
using UnityEngine;

namespace Source.Scripts.GameCore.Battle
{
    public interface IReadOnlyTeam
    {
        bool TryGetNearestUnit(Vector3 currentPosition, IEnumerable<MoveType> attackTargetTypes, out IDamageable unit, out float distanceToCurrentTarget);
        bool TryGetNearestTower(in Vector3 currentPosition, out IDamageable tower, out float distance);
    }
}