﻿using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;

namespace Source.Scripts.UI.Elements
{
    public class SaveDeckButton : ButtonBase
    {
        private IDeckService _deck;
        private IWindowService _windows;

        [Inject]
        private void Construct(IDeckService deck, IWindowService windows)
        {
            _deck = deck;
            _windows = windows;
        }

        private void Awake() => 
            AddListener(OnButtonClicked);

        private void OnDestroy() => 
            RemoveListener(OnButtonClicked);

        private void OnButtonClicked()
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