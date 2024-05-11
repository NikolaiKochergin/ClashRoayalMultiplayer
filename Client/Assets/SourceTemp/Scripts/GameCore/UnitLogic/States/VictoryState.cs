using Source.Scripts.GameCore.States;

namespace Source.Scripts.GameCore.UnitLogic.States
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