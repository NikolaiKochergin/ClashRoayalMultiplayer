using System;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck.StaticData
{
    [Serializable]
    public class CardInfo
    {
        [field: SerializeField, Delayed] public string Name { get; private set; }
        [field: SerializeField, Delayed] public int Id { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField, TextArea(minLines: 2, maxLines: 5)] public string Description { get; private set; }

#if UNITY_EDITOR
        public void Validate() => 
            Name = Name.Trim(' ');
#endif
    }
}