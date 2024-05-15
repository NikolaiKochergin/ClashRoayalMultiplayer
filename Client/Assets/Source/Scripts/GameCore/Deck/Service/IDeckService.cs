using System;
using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.StaticData;

namespace Source.Scripts.GameCore.Deck.Service
{
    public interface IDeckService
    {
        IEnumerable<CardInfo> SelectedCards { get; }
        IEnumerable<CardInfo> AvailableCards { get; }
        event Action Updated;
        bool TrySelect(string cardId);
        void UpdateSelectedCards(Action onSuccess = null, Action<string> onError = null);
        bool TryUnselect(string cardId);
        void LoadDeck(Action onSuccess = null, Action<string> onError = null);
    }
}