using Source.Scripts.Common.States;

namespace Source.Scripts.GameCore.Battle.UnitLogic.States
{
    public class VictoryState : FSMState
    {
        private readonly UnitAnimator _animator;

        public VictoryState(UnitAnimator unitAnimator) => 
            _animator = unitAnimator;

        public override void Enter() => 
            _animator.ShowVictory();
    }
}