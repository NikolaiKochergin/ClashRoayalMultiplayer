using Source.Scripts.GameCore.UnitLogic.UI;
using Source.Scripts.StaticData;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public abstract class UnitBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _projectileAim;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private UnitStats _stats;
        [SerializeField] private ParticleSystem _takeDamageParticles;
        
        private Health _health;

        public virtual void Construct(Team enemyTeam) => 
            _health = new Health(_stats.HealthMaxValue);

        public UnitStats Stats => _stats;
        public Transform Transform => transform;
        public Transform ProjectileAim => _projectileAim;
        public IHealth Health => _health;
        public float Radius => _stats.ModelRadius;

        private void Start() => 
            _healthBar.Initialize(_health);

        public void ApplyDamage(float value)
        {
            if(_takeDamageParticles)
                _takeDamageParticles.Play();
            _health.ApplyDamage(value);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StartChaseDistance);
            Handles.color = Color.black;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StopChaseDistance);
            
            Handles.color = Color.red;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StartAttackDistance);
            Handles.color = Color.magenta;
            Handles.DrawWireDisc(transform.position, Vector3.up, _stats.StopAttackDistance);
        }
#endif
    }
}
