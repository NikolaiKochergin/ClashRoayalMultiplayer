using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class HandInput : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Transform _cardsContainer;
        
        private IDeckService _deck;

        [Inject]
        private void Construct(IDeckService deck) => 
            _deck = deck;

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag.TryGetComponent(out CardInput card) && _deck.TrySelect(card.Id)) 
                card.transform.SetParent(_cardsContainer);
        }
    }
}