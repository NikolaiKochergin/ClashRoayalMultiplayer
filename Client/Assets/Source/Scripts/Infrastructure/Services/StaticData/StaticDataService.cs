using System.Collections.Generic;
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
        private IReadOnlyDictionary<int, CardInfo> _cardsMap;

        public async UniTask Load()
        {
            _gameConfig = await Addressables.LoadAssetAsync<GameConfig>(GameConfig);

            _cardsMap = _gameConfig.Deck.CardsMap;
        }

        public URL ForURL() => 
            _gameConfig.URL;

        public int ForHandCapacity() => 
            _gameConfig.Deck.HandCapacity;

        public CardInfo ForCard(int id) =>
            _cardsMap.GetValueOrDefault(id);
    }
}