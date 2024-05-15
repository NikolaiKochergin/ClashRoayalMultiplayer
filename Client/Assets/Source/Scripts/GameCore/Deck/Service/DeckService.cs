using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.GameCore.Deck.Data;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Infrastructure.Services.Network;
using Source.Scripts.Infrastructure.Services.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.Service
{
    public class DeckService : IDeckService
    {
        private const string UserID = "userID";
        private const string SelectedIDs = "selectedIDs";
        
        private readonly INetworkService _network;
        private readonly IStaticDataService _staticData;
        private readonly IAuthorizationService _authorization;
        private readonly List<CardInfo> _selectedCards = new List<CardInfo>();
        private readonly List<CardInfo> _availableCards = new List<CardInfo>();

        public DeckService(INetworkService network, IStaticDataService staticData, IAuthorizationService authorization)
        {
            _network = network;
            _staticData = staticData;
            _authorization = authorization;
        }
        
        public IEnumerable<CardInfo> SelectedCards => _selectedCards;
        public IEnumerable<CardInfo> AvailableCards => _availableCards;
        
        public event Action Updated;

        public void LoadDeck(Action onSuccess = null, Action<string> onError = null)
        {
            if (_authorization.IsAuthorized == false)
            {
                onError?.Invoke("User is not authorized.");
                return;
            }
            
            Dictionary<string, string> data = new()
            {
                { UserID, _authorization.UserId.ToString() },
            };

            _network.SendRequest(_staticData.ForURL().GetDeck, data, OnSuccess, OnError);
            return;
            
            void OnSuccess(string request)
            {
                string[] result = request.Split('|');
                if (result.Length < 2 || result[0] != "ok")
                {
                    Debug.LogError($"Server request: {request}");
                    onError?.Invoke($"Server request: {request}");
                    return;
                }
                
                DeckData deckData = JsonUtility.FromJson<DeckData>(result[1]);
                
                _selectedCards.Clear();
                _availableCards.Clear();

                foreach (AvailableCards card in deckData.availableCards)
                {
                    if(deckData.selectedIDs.Contains(card.id))
                        _selectedCards.Add(_staticData.ForCard(card.id));
                    else
                        _availableCards.Add(_staticData.ForCard(card.id));
                }

                onSuccess?.Invoke();
                Updated?.Invoke();
            }

            void OnError(string errorMessage)
            {
                Debug.LogError(errorMessage);
                onError?.Invoke(errorMessage);
            }
        }

        public void UpdateSelectedCards(Action onSuccess = null, Action<string> onError = null)
        {
            if (_authorization.IsAuthorized == false)
            {
                onError?.Invoke("User is not authorized.");
                return;
            }
            
            SelectedData selectedData = new()
            {
                IDs = _selectedCards.Select(c => c.Id).ToArray(),
            };
            
            Dictionary<string, string> data = new()
            {
                { UserID, _authorization.UserId.ToString() },
                { SelectedIDs, JsonUtility.ToJson(selectedData) },
            };
            _network.SendRequest(_staticData.ForURL().UpdateDeck, data, OnSuccess, OnError);
            return;

            void OnSuccess(string successData)
            {
                if (successData != "ok")
                {
                    OnError(successData);
                    return;
                }
                onSuccess?.Invoke();
            }
            
            void OnError(string errorMessage)
            {
                Debug.LogError(errorMessage);
                onError?.Invoke(errorMessage);
            }
        }

        public bool TrySelect(CardInfo card)
        {
            if (_selectedCards.Contains(card) || _selectedCards.Count >= _staticData.ForBattleDeckCapacity())
                return false;
            
            _availableCards.Remove(card);
            _selectedCards.Add(card);
            return true;
        }

        public bool TryUnselect(CardInfo card)
        {
            if (_selectedCards.Contains(card) == false) 
                return false;
            
            _selectedCards.Remove(card);
            _availableCards.Add(card);
            return true;
        }
    }
}