﻿using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.StaticData;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService, IStaticDataLoader
    {
        private const string GameConfig = "GameConfig";
        
        private GameConfig _gameConfig;
        private IReadOnlyDictionary<string, CardInfo> _cardsMap;

        public async UniTask Load()
        {
            _gameConfig = await Addressables.LoadAssetAsync<GameConfig>(GameConfig);

            _cardsMap = _gameConfig.Deck.CardsMap;
        }

        public URL ForURL() => 
            _gameConfig.URL;

        public string ForBattleScene() => 
            _gameConfig.BattleScene.AssetGUID;

        public int ForBattleDeckCapacity() => 
            _gameConfig.Deck.BattleDeckCapacity;

        public int ForHandCapacity() =>
            _gameConfig.Deck.HandCapacity;

        public CardInfo ForCard(string id) =>
            _cardsMap.GetValueOrDefault(id);
    }
}