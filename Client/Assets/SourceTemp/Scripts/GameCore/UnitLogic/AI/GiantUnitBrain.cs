using Source.Scripts.GameCore.States;
using Source.Scripts.GameCore.UnitLogic.States;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.AI
{
    public class GiantUnitBrain
    {
        private readonly UnitBase _unit;
        private readonly FSM _fsm;
        private readonly Team _enemyTeam;
        private readonly TargetContainer _target;

        private float _distanceToCurrentTarget;
        
        public GiantUnitBrain(UnitBase unit, FSM fsm, Team enemyTeam, TargetContainer target)
        {
            _unit = unit;
            _fsm = fsm;
            _enemyTeam = enemyTeam;
            _target = target;
            _unit.Health.Died += OnDied;
        }

        public void Update()
        {
            switch (_fsm.CurrentState)
            {
                case SearchTargetState:
                    if (_enemyTeam.TryGetNearestTower(_unit.Transform.position, out _target.Damageable, out _distanceToCurrentTarget))
                    {
                        _fsm.Set<MoveToTargetState>();
                        break;
                    }

                    _target.Damageable = null;
                    _fsm.Set<VictoryState>();
                    break;
                case MoveToTargetState:
                    if (_target.Damageable.Health.CurrentValue == 0)
                    {
                        _fsm.Set<SearchTargetState>();
                        break;
                    }

                    _distanceToCurrentTarget = DistanceTo(_target.Damageable.Transform);
                    if (_unit.Stats.StartAttackDistance + _target.Damageable.Radius >= _distanceToCurrentTarget) 
                        _fsm.Set<AttackState>();
                    break;
                case AttackState:
                    if (_target.Damageable.Health.CurrentValue == 0) 
                        _fsm.Set<SearchTargetState>();
                    break;
                case VictoryState:
                    break;
                case DieState:
                    break;
                default:
                    _fsm.Set<SearchTargetState>();
                    break;
            }
            
            _fsm.Update();
        }

        private void OnDied()
        {
            _unit.Health.Died -= OnDied;
            _fsm.Set<DieState>();
        }

        private float DistanceTo(Transform transform) => 
            Vector3.Distance(_unit.Transform.position, transform.position);
    }
}