using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using UnityEngine;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class EditDeckWindow : WindowBase
    {
        [SerializeField] private DeckView _selectedCardsDeck;
        [SerializeField] private DeckView _availableCardsDeck;
        
        private IDeckService _deck;

        [Inject]
        private void Construct(IDeckService deck) => 
            _deck = deck;

        protected override void Initialize() => 
            DisplayCards();

        private void DisplayCards()
        {
            _selectedCardsDeck.Display(_deck.SelectedCards);
            _availableCardsDeck.Display(_deck.AvailableCards);
        }
    }
}