using DG.Tweening;
using Source.Scripts.Multiplayer.Data;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Countdown
{
    public class CountdownMessage : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Ease _ease;
        [SerializeField, Min(0)] private float _duration = 0.5f;
        [SerializeField, Min(0)] private float _endScale = 1.5f;
        
        private Sequence _sequence;
        private Vector3 _defaultScale;

        private void Awake()
        {
            _canvasGroup.alpha = 0f;
            _defaultScale = transform.localScale;
        }

        private void OnDestroy() => 
            _sequence.Kill();
        
        public void DisplayTick(TickData data)
        {
            _sequence.Kill();
            _text.SetText(10 == data.tick ? "Fight!" : (10 - data.tick).ToString());
            _canvasGroup.alpha = 1f;
            _sequence = DOTween.Sequence()
                .Append(transform.DOScale(_defaultScale * _endScale, _duration).SetEase(_ease))
                .Join(_canvasGroup.DOFade(0, _duration).SetEase(_ease))
                .OnKill(() =>
                {
                    _canvasGroup.alpha = 0f;
                    transform.localScale = _defaultScale;
                });
        }
    }
}