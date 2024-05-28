using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Reflex.Extensions;
using Source.Scripts.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.UnitLogic;
using Source.Scripts.GameCore.Battle.UnitLogic.Data;
using Source.Scripts.GameCore.Battle.UnitLogic.UI;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.Factory;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private const string Spawn = "Spawn";
        private const string Json = "json";
        
        private readonly Team _team = new();
        private readonly List<CardInfo> _battleDeck = new();
        private readonly List<CardInfo> _inHandCards = new();
        private readonly Queue<UnitHologram> _holograms = new();
        private readonly IMultiplayerService _multiplayer;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _factory;

        private IReadOnlyTeam _enemyTeam;
        private int _handCapacity;

        public PlayerService(
            IMultiplayerService multiplayer,
            IStaticDataService staticData,
            IGameFactory factory)
        {
            _multiplayer = multiplayer;
            _staticData = staticData;
            _factory = factory;

            _multiplayer.CheatHappened += CancelSpawn;
            multiplayer.SpawnPlayerHappened += OnSpawnPlayerHappened;
        }

        public IReadOnlyTeam Team => _team;
        public IReadOnlyList<CardInfo> InHandCards => _inHandCards;
        public CardInfo NextCard { get; private set; }

        public event Action NextCardUpdated;

        public void Initialize(IReadOnlyTeam enemyTeam)
        {
            _enemyTeam = enemyTeam;
            _handCapacity = _staticData.ForHandCapacity();
            InitFromMap();
            InitFromMultiplayer();
        }

        public async UniTask SendSpawn(CardInfo card, Vector3 spawnPoint)
        {
            ReplaceCards(card);
            
            UnitHologram hologram = await _factory.Create<UnitHologram>(card.HologramReference);
            hologram.transform.position = spawnPoint;
            _holograms.Enqueue(hologram);

            Dictionary<string, string> data = new Dictionary<string, string>()
            {
                {Json, JsonUtility.ToJson(new SpawnData(card.Id, spawnPoint))}
            };
            
            _multiplayer.SendMessage(Spawn, data);
        }

        private void OnSpawnPlayerHappened(SpawnData spawnData)
        {
            if (_holograms.Count > 0)
            {
                UnitHologram hologram = _holograms.Dequeue();
                Object.Destroy(hologram.gameObject);
            }
            
            CardInfo card = _staticData.ForCard(spawnData.cardID);
            SpawnUnit(card, new Vector3(spawnData.x, spawnData.y, spawnData.z)).Forget();
        }

        private async UniTaskVoid SpawnUnit(CardInfo card, Vector3 spawnPoint)
        {
            UnitBase unit = await _factory.Create<UnitBase>(card.UnitReference);
            unit.transform.position = spawnPoint;
            unit.Construct(_enemyTeam);
            _team.Add(unit);
        }

        private void CancelSpawn()
        {
            if (_holograms.Count < 1) 
                return;
            
            UnitHologram hologram = _holograms.Dequeue();
            Object.Destroy(hologram.gameObject);
        }

        private void ReplaceCards(CardInfo card)
        {
            _inHandCards[_inHandCards.IndexOf(card)] = NextCard;
            _battleDeck.Add(card);
            _battleDeck.Shuffle();
            SetNextCard();
        }

        private void InitFromMultiplayer()
        {
            foreach (string id in _multiplayer.PlayerCardIDs) 
                _battleDeck.Add(_staticData.ForCard(id));
            
            _battleDeck.Shuffle();
            
            _inHandCards.AddRange(_battleDeck.GetRange(0, _handCapacity));
            _battleDeck.RemoveRange(0, _handCapacity);

            SetNextCard();
        }

        private void InitFromMap()
        {
            MapInfo map = GetMapInfo();
            
            foreach (Tower tower in map.Player.Towers)
            {
                tower.Construct();
                _team.Add(tower);
            }

            foreach (UnitBase unit in map.Player.Units)
            {
                unit.Construct(_enemyTeam);
                _team.Add(unit);
            }
        }

        private void SetNextCard()
        {
            NextCard = _battleDeck[0];
            _battleDeck.RemoveAt(0);
            NextCardUpdated?.Invoke();
        }

        private static MapInfo GetMapInfo() => 
            SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
    }
}