using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.UI.CommonElements;
using Source.Scripts.UI.Services.Windows;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public sealed class SaveDeckButton : ButtonBase
    {
        private IDeckService _deck;
        private IWindowService _windows;

        [Inject]
        private void Construct(IDeckService deck, IWindowService windows)
        {
            _deck = deck;
            _windows = windows;
        }

        protected override void OnButtonClicked()
        {
            _windows.OpenWindow(WindowId.LockScreen);
            _deck.UpdateSelectedCards(CloseLockScreen, _ => CloseLockScreen());
        }

        private void CloseLockScreen()
        {
            _windows.CloseWindow(WindowId.LockScreen);
            _windows.OpenWindow(WindowId.StartGame);
        }
    }
}