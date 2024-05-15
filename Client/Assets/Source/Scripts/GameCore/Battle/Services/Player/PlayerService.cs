using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Reflex.Extensions;
using Source.Scripts.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.UnitLogic;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.Factory;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public class PlayerService : IPlayerService
    {
        private readonly Team _team = new();
        private readonly List<CardInfo> _battleDeck = new();
        private readonly List<CardInfo> _inHandCards = new();
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

        public async UniTask SpawnUnit(CardInfo card, Vector3 position)
        {
            _inHandCards[_inHandCards.IndexOf(card)] = NextCard;
            _battleDeck.Add(card);
            _battleDeck.Shuffle();
            SetNextCard();
            
            UnitBase unit = await _factory.Create<UnitBase>(card.UnitReference);
            unit.transform.position = position;
            unit.Construct(_enemyTeam);
            _team.Add(unit);
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