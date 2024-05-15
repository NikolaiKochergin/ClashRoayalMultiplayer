using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.AssetManagement;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.EditDeck
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _description;
        [SerializeField] private Image _icon;
        
        private IAsset _asset;

        public CardInfo Info { get; private set; }

        [Inject]
        private void Construct(IAsset asset) => 
            _asset = asset;

        public void Display(CardInfo cardInfo)
        {
            Info = cardInfo;
            _name.SetText(cardInfo.Name);
            _description.SetText(cardInfo.Description);
            SetIcon(cardInfo.IconReference).Forget();
        }

        private async UniTaskVoid SetIcon(AssetReference iconReference) => 
            _icon.sprite = await _asset.Load<Sprite>(iconReference);
    }
}