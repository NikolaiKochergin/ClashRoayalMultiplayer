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
        private readonly IAuthorizationService _authorization;
        private const string ServerSettings = "ServerSettings";
        private const string RoomName = "game_room";
        private const string GetReady = "GetReady";
        private const string StartGame = "Start";
        private const string CancelStart = "CancelStart";
        
        private ColyseusSettings _serverSettings;
        private ColyseusClient _client;
        private ColyseusRoom<GameRoomState> _room;

        public MultiplayerService(IAuthorizationService authorization) => 
            _authorization = authorization;

        public event Action GetReadyHappened;
        public event Action<Decks> StartGameHappened;
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
            _room.OnMessage<object>(GetReady, _ => GetReadyHappened?.Invoke());
            _room.OnMessage<string>(StartGame, jsonDecks => StartGameHappened?.Invoke(JsonUtility.FromJson<Decks>(jsonDecks)));
            _room.OnMessage<object>(CancelStart, _ => CancelStartHappened?.Invoke());
        }

        public async UniTask CancelConnect()
        {
            await _room.Leave();
            _room = null;
        }

        private async UniTask LoadServerSettings() => 
            _serverSettings = await Addressables.LoadAssetAsync<ColyseusSettings>(ServerSettings);
    }
}