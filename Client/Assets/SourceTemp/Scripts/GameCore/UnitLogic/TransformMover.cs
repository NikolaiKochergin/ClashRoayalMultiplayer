using Source.Scripts.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class TransformMover : MonoBehaviour, IMover
    {
        private float _speed;
        private bool _isStopped;

        public TransformMover Construct(UnitStats stats)
        {
            _speed = stats.Speed;
            return this;
        }

        public void SetDestination(Vector3 position) => 
            transform.LookAt(position, Vector3.up);

        public void StopMove(bool value) => 
            _isStopped = value;

        private void Update()
        {
            if(_isStopped == false)
                transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }
    }
}