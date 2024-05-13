using System;
using System.Collections.Generic;
using Colyseus;
using Cysharp.Threading.Tasks;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Multiplayer.Data;
using Source.Scripts.Multiplayer.Schemas;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.Multiplayer
{
    public class MultiplayerService : IMultiplayerService, IInitializable
    {
        private const string ServerSettings = "ServerSettings";
        private const string RoomName = "game_room";
        private const string GetReady = "GetReady";
        private const string StartGame = "Start";
        private const string CancelStart = "CancelStart";
        
        private readonly IAuthorizationService _authorization;
        
        private ColyseusSettings _serverSettings;
        private ColyseusClient _client;
        private ColyseusRoom<GameRoomState> _room;

        public MultiplayerService(IAuthorizationService authorization) => 
            _authorization = authorization;

        public IReadOnlyList<int> PlayerCardIDs { get; private set; }
        public IReadOnlyList<int> EnemyCardIDs { get; private set; }

        public event Action GetReadyHappened;
        public event Action StartGameHappened;
        public event Action CancelStartHappened;

        public async UniTask Initialize()
        {
            await LoadServerSettings();
            _client = new ColyseusClient(_serverSettings);
        }

        public async UniTask Connect()
        {
            if(_authorization.IsAuthorized == false)
                return;

            Dictionary<string, object> data = new()
            {
                { "id", _authorization.UserId },
            };
            
            _room = await _client.JoinOrCreate<GameRoomState>(RoomName, data);
            _room.OnMessage<string>(GetReady, _ => GetReadyHappened?.Invoke());
            _room.OnMessage<string>(CancelStart, _ => CancelStartHappened?.Invoke());
            _room.OnMessage<string>(StartGame, decksJson =>
            {
                DecksData decksData = JsonUtility.FromJson<DecksData>(decksJson);
                
                if (decksData.player1ID == _room.SessionId)
                {
                    PlayerCardIDs = GetParsedIDs(decksData.player1);
                    EnemyCardIDs = GetParsedIDs(decksData.player2);
                }
                else
                {
                    PlayerCardIDs = GetParsedIDs(decksData.player2);
                    EnemyCardIDs = GetParsedIDs(decksData.player1);
                }
                
                StartGameHappened?.Invoke();
            });
        }

        public async UniTask Leave()
        {
            if(_room == null)
                return;
            
            await _room.Leave();
            _room = null;
        }

        private async UniTask LoadServerSettings() => 
            _serverSettings = await Addressables.LoadAssetAsync<ColyseusSettings>(ServerSettings);

        private static List<int> GetParsedIDs(IEnumerable<string> ids)
        {
            List<int> deck = new();
            foreach (string cardId in ids)
                if(int.TryParse(cardId, out int id))
                    deck.Add(id);
            return deck;
        }
    }
}