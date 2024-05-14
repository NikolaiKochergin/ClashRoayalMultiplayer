using Reflex.Attributes;
using Source.Scripts.UI.Windows;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class CardInput : MonoBehaviour, IBeginDragHandler, IDragHandler , IEndDragHandler
    {
        [SerializeField] private CardView _cardView;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private UIRoot _uiRoot;
        public int Id => _cardView.Id;
        
        [Inject]
        private void Construct(UIRoot uiRoot) => 
            _uiRoot = uiRoot;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _cardView.transform.SetParent(_uiRoot.transform);
            _cardView.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) =>
            ((RectTransform) _cardView.transform).anchoredPosition += eventData.delta / _uiRoot.ScaleFactor;

        public void OnEndDrag(PointerEventData eventData)
        {
            _cardView.transform.SetParent(transform);
            _canvasGroup.blocksRaycasts = true;
            _cardView.transform.localPosition = Vector3.zero;
        }
    }
}