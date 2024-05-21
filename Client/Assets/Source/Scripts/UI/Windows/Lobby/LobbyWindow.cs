using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.Multiplayer;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Lobby
{
    public class LobbyWindow : WindowBase
    {
        [SerializeField] private DeckView _deckView;
        [SerializeField] private GameObject _buttons;
        [SerializeField] private GameObject _matchmakingPopup;
        
        private IDeckService _deck;
        private IMultiplayerService _multiplayer;

        [Inject]
        private void Construct(IDeckService deck, IMultiplayerService multiplayer)
        {
            _deck = deck;
            _multiplayer = multiplayer;
        }

        protected override void Initialize() => 
            DisplayCards();

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _multiplayer.CancelStartHappened += OnCancelStartHappened;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _multiplayer.CancelStartHappened -= OnCancelStartHappened;
        }

        private void OnCancelStartHappened()
        {
            _matchmakingPopup.SetActive(false);
            _buttons.SetActive(true);
        }

        private void DisplayCards() => 
            _deckView.Display(_deck.SelectedCards);
    }
}