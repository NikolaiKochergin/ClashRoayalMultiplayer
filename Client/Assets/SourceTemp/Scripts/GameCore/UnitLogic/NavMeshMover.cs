using Source.Scripts.StaticData;
using UnityEngine;
using UnityEngine.AI;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class NavMeshMover : IMover
    {
        private readonly NavMeshAgent _agent;

        public NavMeshMover(NavMeshAgent agent, UnitStats stats)
        {
            _agent = agent;
            _agent.stoppingDistance = stats.StartAttackDistance;
            _agent.speed = stats.Speed;
            _agent.radius = stats.ModelRadius;
        }

        public void SetDestination(Vector3 position) => 
            _agent.SetDestination(position);

        public void StopMove(bool value) => 
            _agent.isStopped = value;
    }
}