using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Source.Scripts.GameCore.Deck.StaticData;
using Source.Scripts.Infrastructure.Services.AssetManagement;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.Battle
{
    public class NextUnitPreview : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        
        private IAsset _asset;

        [Inject]
        private void Construct(IAsset asset) =>
            _asset = asset;
        
        public void Display(CardInfo cardInfo) => 
            SetIcon(cardInfo.IconReference).Forget();

        private async UniTaskVoid SetIcon(AssetReference iconReference) => 
            _icon.sprite = await _asset.Load<Sprite>(iconReference);
    }
}