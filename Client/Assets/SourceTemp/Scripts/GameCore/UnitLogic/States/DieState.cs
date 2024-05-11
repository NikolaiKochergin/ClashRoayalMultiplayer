using Source.Scripts.GameCore.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.States
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