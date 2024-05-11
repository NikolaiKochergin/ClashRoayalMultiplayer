using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public interface ITarget
    {
        Transform Transform { get; }
        Transform ProjectileAim { get; }
        float Radius { get; }
    }
}