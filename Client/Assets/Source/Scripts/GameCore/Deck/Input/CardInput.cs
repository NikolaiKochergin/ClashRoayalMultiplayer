using Reflex.Attributes;
using Source.Scripts.GameCore.Battle.Services.Player;
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
        private IPlayerService _player;
        public string Id => _cardView.Id;
        
        [Inject]
        private void Construct(UIRoot uiRoot, IPlayerService player)
        {
            _uiRoot = uiRoot;
            _player = player;
        }

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
            _cardView.transform.localPosition = Vector3.zero;

            if (TryGetSpawnPosition(out Vector3 position))
            {
                string unitId = Id;
                _cardView.Display(_player.NextCard);
                _player.SpawnUnit(unitId, position);
            }
            
            _canvasGroup.blocksRaycasts = true;
        }

        private bool TryGetSpawnPosition(out Vector3 position)
        {

            position = Vector3.zero;

            return true;
        }
    }
}