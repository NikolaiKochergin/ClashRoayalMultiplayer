﻿using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.Service;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Source.Scripts.GameCore.Deck.Input
{
    public class HandInput : MonoBehaviour, IDropHandler
    {
        [SerializeField] private Transform _cardsContainer;
        
        private IDeckService _deck;

        [Inject]
        private void Construct(IDeckService deck) => 
            _deck = deck;

        public void OnDrop(PointerEventData eventData)
        {
            Debug.Log(">>>>>>>>>>>>>>");
            if (eventData.pointerDrag.TryGetComponent(out EditDeckCardInput card) && _deck.TrySelect(card.Info)) 
                card.transform.SetParent(_cardsContainer);
        }
    }
}