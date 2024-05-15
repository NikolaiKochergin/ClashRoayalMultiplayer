using Reflex.Attributes;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.UI.Windows.EditDeck;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Battle
{
    public class BattlePanel : MonoBehaviour
    {
        [SerializeField] private DeckView _battleDeck;
        [SerializeField] private NextUnitPreview _nextUnitPreview;
        
        private IPlayerService _player;

        [Inject]
        private void Construct(IPlayerService player) => 
            _player = player;

        private void Start()
        {
            DisplayCards();
            _player.NextCardUpdated += DisplayNextCard;
        }

        private void OnDestroy() => 
            _player.NextCardUpdated -= DisplayNextCard;

        private void DisplayCards()
        {
            _battleDeck.Display(_player.InHandCards);
            DisplayNextCard();
        }

        private void DisplayNextCard() => 
            _nextUnitPreview.Display(_player.NextCard);
    }
}