using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.StaticData
{
    [Serializable]
    public class DeckConfig
    {
        [field: SerializeField, Min(1)] public int HandCapacity = 4;
        [field: SerializeField, Min(2)] public int BattleDeckCapacity = 8;
        [SerializeField] private List<CardInfo> _cards;

        public IReadOnlyDictionary<string, CardInfo> CardsMap =>
            _cards.ToDictionary(card => card.Id, card => card);
        
#if UNITY_EDITOR
        public void Validate()
        {
            foreach (CardInfo card in _cards) 
                card.Validate();

            if (BattleDeckCapacity < HandCapacity + 1)
                BattleDeckCapacity = HandCapacity + 1;

            if (BattleDeckCapacity > _cards.Count)
            {
                Debug.LogError("The capacity of the deck should not be greater than the number of available cards.");
                BattleDeckCapacity = _cards.Count;
            }
        } 
#endif
    }
}