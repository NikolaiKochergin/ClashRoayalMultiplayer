using Source.Scripts.GameCore.States;
using Source.Scripts.GameCore.UnitLogic.AI;
using Source.Scripts.GameCore.UnitLogic.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class MeleeFlyingUnit : UnitBase
    {
        [SerializeField] private TransformMover _transformMover;
        [SerializeField] private UnitAnimator _unitAnimator;
        [SerializeField] private AttackBase _attack;
        
        private FSM _fsm;
        private IMover _mover;
        private TargetContainer _target;
        private MeleeUnitBrain _brain;
        
        public override void Construct(Team enemyTeam)
        {
            base.Construct(enemyTeam);
            _attack.Construct(Stats);
            _mover = _transformMover.Construct(Stats);
            _target = new TargetContainer();
            _fsm = new FSMBuilder()
                .Add(new SearchTargetState())
                .Add(new MoveToTargetState(_unitAnimator, _mover, _target))
                .Add(new AttackState(_unitAnimator, _attack, _target))
                .Add(new ChaseState(_unitAnimator, _mover, _target))
                .Add(new VictoryState(_unitAnimator))
                .Add(new DieState(_unitAnimator))
                .Build();
            _brain = new MeleeUnitBrain(this, _fsm, enemyTeam, _target, _attack);
        }
        
        private void Update()
        {
            if(Health.CurrentValue != 0)
                _brain.Update();
        }
    }
}