using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class EditDeckCardAnimator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] private LayoutElement _layoutElement;
        [SerializeField] private Vector2 _maxSize;
        [SerializeField, Min(0)] private float _expandDuration = 0.5f;
        
        private TweenerCore<Vector2, Vector2, VectorOptions> _tween;

        public void OnPointerEnter(PointerEventData eventData) => 
            ExpandSize(_maxSize);

        public void OnPointerExit(PointerEventData eventData) => 
            ExpandSize(Vector2.zero);

        public void OnPointerDown(PointerEventData eventData) => 
            ExpandSize(Vector2.zero);

        private void ExpandSize(Vector2 value)
        {
            _tween.Kill();
            _tween = _layoutElement.DOMinSize(value, _expandDuration);
        }
    }
}