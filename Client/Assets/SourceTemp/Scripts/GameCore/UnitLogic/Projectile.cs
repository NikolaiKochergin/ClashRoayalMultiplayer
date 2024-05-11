using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _speed;
        
        private IDamageable _target;
        private float _damage;

        public void Construct(IDamageable target, float damage)
        {
            _target = target;
            _damage = damage;
        }

        private void Update()
        {
            if (_target.Health.CurrentValue == 0)
            {
                Destroy(gameObject);
                return;
            }

            float maxDistanceDelta = _speed * Time.deltaTime;
            transform.position = Vector3
                .MoveTowards(
                    transform.position,
                    _target.ProjectileAim.position,
                    maxDistanceDelta);

            if (Vector3.Distance(transform.position, _target.ProjectileAim.position) > maxDistanceDelta) 
                return;
            
            _target.ApplyDamage(_damage);
            Destroy(gameObject);
        }
    }
}