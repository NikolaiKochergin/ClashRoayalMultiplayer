using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.Cameras;
using Source.Scripts.UI.Windows;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class EditDeckCardInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Card _card;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LayoutElement _layoutElement;

        private UIRoot _uiRoot;
        private TweenerCore<Vector2, Vector2, VectorOptions> _tween;

        [Inject]
        private void Construct(UIRoot uiRoot) => 
            _uiRoot = uiRoot;

        public CardInfo Info => _card.Info;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _canvasGroup.blocksRaycasts = false;
            _card.transform.SetParent(_uiRoot.transform);
            _card.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) => 
            ((RectTransform) _card.transform).anchoredPosition += eventData.delta / _uiRoot.ScaleFactor;

        public void OnEndDrag(PointerEventData eventData)
        {
            _card.transform.SetParent(transform);
            _card.transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween.Kill();
            _tween = _layoutElement.DOMinSize(new Vector2(415, _layoutElement.minHeight), 1);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween.Kill();
            _tween = _layoutElement.DOMinSize(Vector2.zero, 1);
        }
    }
}