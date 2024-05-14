using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck
{
    public class CardDragAnimation : MonoBehaviour, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform _cardViewTransform;
        [SerializeField] private float _scale = 0.8f;
        
        private Vector3 _defaultScale;

        private void Awake() => 
            _defaultScale = _cardViewTransform.localScale;

        public void OnBeginDrag(PointerEventData eventData) => 
            _cardViewTransform.localScale = _cardViewTransform.lossyScale * _scale;

        public void OnEndDrag(PointerEventData eventData) => 
            _cardViewTransform.localScale =_defaultScale;
    }
}