using System;

namespace Source.Scripts.GameCore.Deck.Data
{
    [Serializable]
    public class DeckData
    {
        public AvailableCards[] availableCards;
        public string[] selectedIDs;
    }
}