using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.UI.Windows;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class EditDeckCardInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private Card _card;
        [SerializeField] private CanvasGroup _canvasGroup;

        private UIRoot _uiRoot;
        private TweenerCore<Vector2, Vector2, VectorOptions> _tween;


        [Inject]
        private void Construct(UIRoot uiRoot) => 
            _uiRoot = uiRoot;

        public CardInfo Info => _card.Info;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _tween.Kill();
            _canvasGroup.blocksRaycasts = false;
            _card.transform.SetParent(_uiRoot.transform);
            _card.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData) => 
            ((RectTransform) _card.transform).anchoredPosition += eventData.delta / _uiRoot.ScaleFactor;

        public void OnEndDrag(PointerEventData eventData) => 
            Invoke(nameof(MoveToPlace), 0.1f);

        private void OnDestroy() => 
            _tween.Kill();

        private void MoveToPlace()
        {
            _card.transform.SetParent(transform);
            _tween = ((RectTransform) _card.transform)
                .DOAnchorPos(Vector2.zero, 0.4f)
                .OnKill(() =>
                {
                    ((RectTransform) _card.transform).anchoredPosition = Vector2.zero;
                    _canvasGroup.blocksRaycasts = true;
                });
        }
    }
}