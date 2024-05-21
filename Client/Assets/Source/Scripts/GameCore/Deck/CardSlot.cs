using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.GameCore.Deck
{
    public class CardSlot : MonoBehaviour
    {
        [FormerlySerializedAs("_cardView")] [SerializeField] private Card _card;
        
        public void Initialize(CardInfo cardInfo) =>
            _card.Display(cardInfo);
    }
}
