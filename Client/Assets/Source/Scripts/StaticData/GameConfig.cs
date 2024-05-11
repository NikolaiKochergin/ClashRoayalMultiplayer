using System.Collections.Generic;
using Source.Scripts.GameCore.Deck.StaticData;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Static Data/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public URL URL { get; private set; }
        [field: SerializeField] public DeckConfig Deck { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            IReadOnlyDictionary<int, CardInfo> map = Deck.CardsMap;
            if(map.ContainsKey(0))
                Debug.LogError($"Cards map contain key: {0}.");
        }
#endif
    }
}