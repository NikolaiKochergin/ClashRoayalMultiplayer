using Reflex.Attributes;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.GameCore.Deck.StaticData;
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
        [SerializeField] private LayerMask _mask;
        
        private UIRoot _uiRoot;
        private IPlayerService _player;
        private Camera _camera;
        public CardInfo Info => _cardView.Info;

        private void Awake() => 
            _camera = Camera.main;

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

            if (TryGetSpawnPositionFor(eventData.position, out Vector3 spawnPosition))
            {
                CardInfo cardInfo = _cardView.Info;
                _cardView.Display(_player.NextCard);
                _player.SpawnUnit(cardInfo, spawnPosition);
            }
            
            _canvasGroup.blocksRaycasts = true;
        }

        private bool TryGetSpawnPositionFor(Vector3 screenPosition, out Vector3 position)
        {
            Ray ray = _camera.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit) && hit.collider.gameObject.layer == 6)
            {
                position = hit.point;
                return true;
            }
                
            position = Vector3.zero;
            return false;
        }
    }
}