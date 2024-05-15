using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.StaticData
{
    [Serializable]
    public class DeckConfig
    {
        [field: SerializeField, Min(0)] public int HandCapacity = 4;
        [field: SerializeField, Min(0)] public int BattleDeckCapacity = 8;
        [SerializeField] private List<CardInfo> _cards;

        public IReadOnlyDictionary<int, CardInfo> CardsMap =>
            _cards.ToDictionary(card => card.Id, card => card);
        
#if UNITY_EDITOR
        public void Validate()
        {
            foreach (CardInfo card in _cards) 
                card.Validate();
        } 
#endif
    }
}