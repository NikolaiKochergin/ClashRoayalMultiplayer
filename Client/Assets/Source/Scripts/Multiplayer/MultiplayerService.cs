using System;
using System.Collections.Generic;
using Colyseus;
using Cysharp.Threading.Tasks;
using Source.Scripts.GameCore.Battle.UnitLogic.Data;
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
        private const string StartTick = "StartTick";
        private const string SpawnPlayer = "SpawnPlayer";
        private const string SpawnEnemy = "SpawnEnemy";
        private const string Cheat = "Cheat";
        
        private readonly IAuthorizationService _authorization;
        
        private ColyseusSettings _serverSettings;
        private ColyseusClient _client;
        private ColyseusRoom<GameRoomState> _room;
        private float _offset = 0;

        public MultiplayerService(IAuthorizationService authorization) => 
            _authorization = authorization;

        public IReadOnlyList<string> PlayerCardIDs { get; private set; }
        public IReadOnlyList<string> EnemyCardIDs { get; private set; }

        public event Action GetReadyHappened;
        public event Action<TickData> StartTickHappened;
        public event Action<SpawnData> SpawnPlayerHappened;
        public event Action<SpawnData> SpawnEnemyHappened;
        public event Action StartGameHappened;
        public event Action CancelStartHappened;
        public event Action CheatHappened;

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
            _room.OnMessage<string>(StartTick, tick => StartTickHappened?.Invoke(TickData(tick)));
            _room.OnMessage<string>(SpawnPlayer, spawnData => SpawnPlayerHappened?.Invoke(JsonUtility.FromJson<SpawnData>(spawnData)));
            _room.OnMessage<string>(SpawnEnemy, spawnData => SpawnEnemyHappened?.Invoke(JsonUtility.FromJson<SpawnData>(spawnData)));
            _room.OnMessage<string>(CancelStart, _ => CancelStartHappened?.Invoke());
            _room.OnMessage<string>(Cheat, _ => CheatHappened?.Invoke());
            _room.OnMessage<string>(StartGame, decksJson =>
            {
                DecksData decksData = JsonUtility.FromJson<DecksData>(decksJson);
                
                if (decksData.player1ID == _room.SessionId)
                {
                    PlayerCardIDs = decksData.player1;
                    EnemyCardIDs = decksData.player2;
                }
                else
                {
                    PlayerCardIDs = decksData.player2;
                    EnemyCardIDs = decksData.player1;
                }
                
                StartGameHappened?.Invoke();
            });
        }

        public void SendMessage(string key, Dictionary<string, string> data) => 
            _room.Send(key, data);

        public float GetConvertedTime(uint serverTime) =>
            serverTime / 1000f + _offset;

        private TickData TickData(string tick)
        {
            TickData data = JsonUtility.FromJson<TickData>(tick);
            if (data.tick < 10)
                return data;

            float gameTime = Time.time;
            float serverTime = data.time / 1000f;

            _offset = gameTime - serverTime;
            
            return data;
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
    }
}