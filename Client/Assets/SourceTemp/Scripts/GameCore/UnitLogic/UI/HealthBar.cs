using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private SpriteSlider _spriteSlider;
        [SerializeField] private HealthFade _healthFade;
        
        private IHealth _health;

        public void Initialize(IHealth health)
        {
            _health = health;
            DisplayHealth();
            _health.Changed += DisplayHealth;
            health.Changed += _healthFade.Display;
        }

        private void OnDestroy()
        {
            _health.Changed -= DisplayHealth;
            _health.Changed -= _healthFade.Display;
        }

        private void DisplayHealth() => 
            _spriteSlider.SetFill(_health.CurrentValue / _health.MaxValue);
    }
}