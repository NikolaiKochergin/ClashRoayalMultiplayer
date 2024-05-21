using System.Collections.Generic;
using Reflex.Attributes;
using Source.Scripts.GameCore.Deck;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.UI.Factory;
using UnityEngine;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private CardSlot _cardPrefab;
        [SerializeField] private Transform _cardsContainer;
        
        private IUIFactory _factory;

        [Inject]
        private void Construct(IUIFactory factory) => 
            _factory = factory;

        public void Display(IEnumerable<CardInfo> cards)
        {
            foreach (CardInfo card in cards) 
                _factory
                    .Create(_cardPrefab, _cardsContainer)
                    .Initialize(card);
        }
    }
}