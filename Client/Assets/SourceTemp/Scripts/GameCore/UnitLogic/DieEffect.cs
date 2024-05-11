using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic
{
    public class DieEffect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _dieParticlesPrefab;
        [SerializeField] private MonoBehaviour _damageable;

        private void Start()
        {
            if(_damageable)
                ((IDamageable) _damageable).Health.Died += OnUnitDied;
        }

        private void OnUnitDied()
        {
            ((IDamageable)_damageable).Health.Died -= OnUnitDied;
            SpawnDieParticles();
        }

        private void SpawnDieParticles() => 
            Instantiate(_dieParticlesPrefab, transform.position + Vector3.up * 1.7f, Quaternion.identity);

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(_damageable == null)
                return;
            
            if (_damageable is IDamageable)
                return;
            
            _damageable = null;
            Debug.LogError("Damageable must be implemented as IDamageable.");
        }
#endif
    }
}