using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public interface IMover
    {
        void SetDestination(Vector3 position);
        void StopMove(bool value);
    }
}