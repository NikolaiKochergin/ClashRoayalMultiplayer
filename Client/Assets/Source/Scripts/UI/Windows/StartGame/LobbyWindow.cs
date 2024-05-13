using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;

namespace Source.Scripts.UI.Windows.StartGame
{
    public class LobbyWindow : WindowBase
    {
        [SerializeField] private DeckView _deckView;
        
        private IDeckService _deck;

        [Inject]
        private void Construct(IDeckService deck) => 
            _deck = deck;

        protected override void Initialize() => 
            DisplayCards();

        private void DisplayCards() => 
            _deckView.Display(_deck.SelectedCards);
    }
}