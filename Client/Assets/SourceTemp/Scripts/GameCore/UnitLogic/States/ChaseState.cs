using Source.Scripts.GameCore.States;

namespace Source.Scripts.GameCore.UnitLogic.States
{
    public class ChaseState : FSMState
    {
        private readonly UnitAnimator _animator;
        private readonly IMover _mover;
        private readonly TargetContainer _targetContainer;

        public ChaseState(UnitAnimator unitAnimator, IMover mover, TargetContainer targetContainer)
        {
            _animator = unitAnimator;
            _mover = mover;
            _targetContainer = targetContainer;
        }

        public override void Enter()
        {
            _animator.ShowRun();
            _mover.StopMove(false);
            _mover.SetDestination(_targetContainer.Damageable.Transform.position);
        }

        public override void Update() => 
            _mover.SetDestination(_targetContainer.Damageable.Transform.position);

        public override void Exit() =>
            _mover.StopMove(true);
    }
}