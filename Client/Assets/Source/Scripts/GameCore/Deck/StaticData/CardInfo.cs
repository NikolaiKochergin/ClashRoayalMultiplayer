using System;
using Source.Scripts.GameCore.Battle.UnitLogic;
using Source.Scripts.StaticData.References;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.GameCore.Deck.StaticData
{
    [Serializable]
    public class CardInfo
    {
        [field: SerializeField, Delayed] public string Name { get; private set; }
        [field: SerializeField, Delayed] public string Id { get; private set; }
        [field: SerializeField] public AssetReferenceSprite IconReference { get; private set; }
        [field: SerializeField] public ComponentReference<UnitBase> UnitReference { get; private set; }
        [field: SerializeField, TextArea(minLines: 2, maxLines: 5)] public string Description { get; private set; }

#if UNITY_EDITOR
        public void Validate()
        {
            Name = Name.Trim();
            Id = Id.Trim();
        }
#endif
    }
}