using Source.Scripts.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class RangeAttack : AttackBase
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _shootPoint;
        
        private float _delay;
        private float _damage;

        public override void Construct(UnitStats stats)
        {
            _delay = stats.AttackDelay;
            _damage = stats.Damage;
        }

        public override float Delay => _delay;

        public override void ApplyTo(IDamageable target)
        {
            if(target == null || target.Health.CurrentValue == 0)
                return;
            
            Projectile projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
            projectile.Construct(target, _damage);
        }
    }
}