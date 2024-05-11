using DG.Tweening;
using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.UI
{
    public class HealthFade : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer[] _spriteRenderers;
        [SerializeField] private float _fadeInDuration = 0.2f;
        [SerializeField] private float _holdDuration = 1.5f;
        [SerializeField] private float _fadeOutDuration = 1f;
        
        private Sequence[] _sequences;

        private void Awake()
        {
            _sequences = new Sequence[_spriteRenderers.Length];
            foreach (SpriteRenderer renderer in _spriteRenderers)
            {
                Color color = renderer.color;
                color.a = 0f;
                renderer.color = color;
            }
        }

        private void OnDestroy()
        {
            foreach (Sequence sequence in _sequences) 
                sequence.Kill();
        }

        public void Display()
        {
            for (int i = 0; i < _spriteRenderers.Length; i++)
            {
                _sequences[i].Kill();
                _sequences[i] = DOTween.Sequence()
                    .Append(_spriteRenderers[i].DOFade(1f, _fadeInDuration))
                    .Append(_spriteRenderers[i].DOFade(0f, _fadeOutDuration).SetDelay(_holdDuration));
            }
        }
    }
}