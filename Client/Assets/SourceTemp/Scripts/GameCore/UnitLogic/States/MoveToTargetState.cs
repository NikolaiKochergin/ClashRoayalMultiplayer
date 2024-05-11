using Source.Scripts.GameCore.States;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class MoveToTargetState : FSMState
    {
        private readonly UnitAnimator _animator;
        private readonly IMover _mover;
        private readonly TargetContainer _targetContainer;

        private ITarget _nearestTower;

        public MoveToTargetState(UnitAnimator unitAnimator, IMover mover, TargetContainer targetContainer)
        {
            _animator = unitAnimator;
            _mover = mover;
            _targetContainer = targetContainer;
        }

        public override void Enter()
        {
            _mover.StopMove(false);
            _mover.SetDestination(_targetContainer.Damageable.Transform.position);
            _animator.ShowRun();
        }

        public override void Exit() =>
            _mover.StopMove(true);
    }
}