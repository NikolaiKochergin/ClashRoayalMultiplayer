using System.Collections.Generic;
using Source.Scripts.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public abstract class AttackBase : MonoBehaviour
    {
        [SerializeField] private MoveType[] _targetTypes;

        public IReadOnlyList<MoveType> TargetTypes => _targetTypes;
        public abstract float Delay { get; }

        public abstract void Construct(UnitStats stats);
        public abstract void ApplyTo(IDamageable target);
    }
}