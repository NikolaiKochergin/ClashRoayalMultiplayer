using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.Infrastructure.Services.Rating;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class LobbyState : IState
    {
        private readonly IWindowService _windows;
        private readonly IDeckService _deck;
        private readonly IRatingService _rating;

        public LobbyState(IWindowService windows, IDeckService deck, IRatingService rating)
        {
            _windows = windows;
            _deck = deck;
            _rating = rating;
        }

        public void Enter()
        {
            _windows.OpenWindow(WindowId.LockScreen);
            _deck.LoadDeck(OnDeckLoaded);
            _rating.LoadRating();
        }

        public void Exit()
        {
        }

        private void OnDeckLoaded()
        {
            _windows.CloseWindow(WindowId.LockScreen);
            _windows.OpenWindow(WindowId.Lobby);
        }
    }
}