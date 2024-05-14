using System.Linq;
using Reflex.Attributes;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Multiplayer;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Battle
{
    public class BattlePanel : MonoBehaviour
    {
        [SerializeField] private DeckView _battleDeck;
        [SerializeField] private NextUnitPreview _nextUnitPreview;
        
        private IMultiplayerService _multiplayer;
        private IStaticDataService _staticData;

        [Inject]
        private void Construct(IMultiplayerService multiplayer, IStaticDataService staticData)
        {
            _multiplayer = multiplayer;
            _staticData = staticData;
        }

        private void Start() => 
            DisplayCards();

        private void DisplayCards()
        {
            _battleDeck.Display(_multiplayer.PlayerCardIDs.Select(id => _staticData.ForCard(id)));

            _nextUnitPreview.Display(_staticData.ForCard(_multiplayer.PlayerCardIDs[0]));
        }

    }
}