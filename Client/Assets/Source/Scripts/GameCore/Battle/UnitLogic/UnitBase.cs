using System;
using Source.Scripts.GameCore.Battle.StaticData;
using Source.Scripts.GameCore.Battle.UnitLogic.UI;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.GameCore.Battle.UnitLogic
{
    public abstract class UnitBase : MonoBehaviour, IDamageable
    {
        [SerializeField] private Transform _projectileAim;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private UnitStats _stats;
        [SerializeField] private ParticleSystem _takeDamageParticles;
        
        private Health _health;

        public virtual void Construct(Team enemyTeam) { }

        public UnitStats Stats => _stats;
        public Transform Transform => transform;
        public Transform ProjectileAim => _projectileAim;
        public IHealth Health => _health;
        public float Radius => _stats.ModelRadius;

        private void Awake()
        {
            _health = new Health(_stats.HealthMaxValue);
            _healthBar.Initialize(_health);
        }

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
