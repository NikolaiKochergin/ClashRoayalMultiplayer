using System;
using Cysharp.Threading.Tasks;
using Reflex.Extensions;
using Source.Scripts.GameCore.Battle.MapLogic;
using Source.Scripts.GameCore.Battle.UnitLogic;
using Source.Scripts.GameCore.Battle.UnitLogic.Data;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.Factory;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Multiplayer;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.GameCore.Battle.Services.Enemy
{
    public class EnemyService : IEnemyService
    {
        private readonly IMultiplayerService _multiplayer;
        private readonly IStaticDataService _staticData;
        private readonly IGameFactory _factory;
        private readonly Team _team = new();
        private IReadOnlyTeam _playerTeam;
        public IReadOnlyTeam Team => _team;

        public EnemyService(
            IMultiplayerService multiplayer, 
            IStaticDataService staticData,
            IGameFactory factory)
        {
            _multiplayer = multiplayer;
            _staticData = staticData;
            _factory = factory;
            _multiplayer.SpawnEnemyHappened += OnSpawnEnemyHappened;
        }

        public void Initialize(IReadOnlyTeam playerTeam)
        {
            _playerTeam = playerTeam;
            MapInfo map = GetMapInfo();
            
            foreach (Tower tower in map.Enemy.Towers)
            {
                tower.Construct();
                _team.Add(tower);
            }

            foreach (UnitBase unit in map.Enemy.Units)
            {
                unit.Construct(playerTeam);
                _team.Add(unit);
            } 
        }

        private void OnSpawnEnemyHappened(SpawnData spawnData)
        {
            CardInfo card = _staticData.ForCard(spawnData.cardID);
            float spawnDelay = _multiplayer.GetConvertedTime(spawnData.serverTime) - Time.time;
            if(spawnDelay < 0)
                Debug.LogError("Недостижимая величина задержки.");
            else
                SpawnUnit(card, new Vector3(spawnData.x, spawnData.y, spawnData.z), spawnDelay).Forget();
        }
        
        private async UniTaskVoid SpawnUnit(CardInfo card, Vector3 spawnPoint, double spawnDelay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(spawnDelay));
            
            UnitBase unit = await _factory.Create<UnitBase>(card.UnitReference);
            unit.Warp(spawnPoint * -1);
            unit.transform.rotation = Quaternion.Euler(0, 180, 0);
            unit.Construct(_playerTeam);
            _team.Add(unit);
        }

        private static MapInfo GetMapInfo() => 
            SceneManager.GetActiveScene().GetSceneContainer().Single<MapInfo>();
    }
}