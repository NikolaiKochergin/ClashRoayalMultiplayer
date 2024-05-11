using Source.Scripts.GameCore.UnitLogic.UI;
using UnityEditor;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class Tower : MonoBehaviour , IDamageable
    {
        [SerializeField] private Transform _projectileAim;
        [SerializeField] private HealthBar _healthBar;
        [SerializeField, Min(0)] private float _healthMaxValue = 20f;
        [SerializeField, Min(0)] private float _radius = 2f;

        private Health _health;

        public Transform Transform => transform;
        public Transform ProjectileAim => _projectileAim;
        public float Radius => _radius;
        public IHealth Health => _health;

        public void Construct() => 
            _health = new Health(_healthMaxValue);

        private void Start()
        {
            _healthBar.Initialize(_health);
            _health.Died += OnDied;
        }

        public void ApplyDamage(float value) => 
            _health.ApplyDamage(value);

        private void OnDied()
        {
            _health.Died -= OnDied;
            Destroy(gameObject);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Handles.color = Color.green;
            Handles.DrawWireDisc(transform.position, Vector3.up, _radius);
        }
#endif
    }
}