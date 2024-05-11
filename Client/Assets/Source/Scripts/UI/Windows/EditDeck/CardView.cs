using Source.Scripts.GameCore.Deck.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;
        
        public int Id { get; private set; }

        public void Display(CardInfo cardInfo)
        {
            Id = cardInfo.Id;
            _name.SetText(cardInfo.Name);
            _description.SetText(cardInfo.Description);
            _icon.sprite = cardInfo.Icon;
        }
    }
}