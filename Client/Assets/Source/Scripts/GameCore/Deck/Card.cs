using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;

namespace Source.Scripts.GameCore.Deck
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardView _cardView;
        
        public void Initialize(CardInfo cardInfo) =>
            _cardView.Display(cardInfo);
    }
}
