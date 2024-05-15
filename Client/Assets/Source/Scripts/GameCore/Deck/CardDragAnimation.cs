using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck
{
    public class CardDragAnimation : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform _cardViewTransform;
        [SerializeField, Min(0)] private float _scale = 0.8f;
        [SerializeField, Min(0)] private float _duration = 0.1f;
        
        private Vector3 _defaultScale;
        private TweenerCore<Vector3, Vector3, VectorOptions> _tween;

        private void Awake() => 
            _defaultScale = _cardViewTransform.localScale;

        private void OnDestroy() => 
            _tween.Kill();

        public void OnBeginDrag(PointerEventData eventData) =>
            _tween = _cardViewTransform
                .DOScale(_cardViewTransform.localScale * _scale, _duration);

        public void OnEndDrag(PointerEventData eventData)
        {
            _tween.Kill();
            _cardViewTransform.localScale = _defaultScale;
        }
    }
}