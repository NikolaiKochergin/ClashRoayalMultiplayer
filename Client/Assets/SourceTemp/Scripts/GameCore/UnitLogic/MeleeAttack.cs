using Source.Scripts.StaticData;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class MeleeAttack : AttackBase
    {
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
            
            target.ApplyDamage(_damage);
        }
    }
}