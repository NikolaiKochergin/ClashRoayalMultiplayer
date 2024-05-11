using UnityEngine;

namespace Source.Scripts.GameCore.Battle.UnitLogic
{
    public interface IMover
    {
        void SetDestination(Vector3 position);
        void StopMove(bool value);
    }
}