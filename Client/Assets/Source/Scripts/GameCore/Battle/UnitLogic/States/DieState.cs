using Source.Scripts.Common.States;
using UnityEngine;

namespace Source.Scripts.GameCore.Battle.UnitLogic.States
{
    public class DieState : FSMState
    {
        private readonly UnitAnimator _animator;

        public DieState(UnitAnimator unitAnimator) => 
            _animator = unitAnimator;

        public override void Enter() => 
            _animator.ShowDie(()=> Object.Destroy(_animator.gameObject));
    }
}