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
        bool TrySelect(CardInfo card);
        void UpdateSelectedCards(Action onSuccess = null, Action<string> onError = null);
        bool TryUnselect(CardInfo card);
        void LoadDeck(Action onSuccess = null, Action<string> onError = null);
    }
}