using Reflex.Attributes;
using Source.Scripts.Infrastructure.States;
using Source.Scripts.Infrastructure.States.Machine;
using Source.Scripts.Multiplayer;
using Source.Scripts.UI.CommonElements;

namespace Source.Scripts.UI.Windows.Lobby
{
    public sealed class CancelButton : ButtonBase
    {
        private IMultiplayerService _multiplayer;
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IMultiplayerService multiplayer, IGameStateMachine gameStateMachine)
        {
            _multiplayer = multiplayer;
            _gameStateMachine = gameStateMachine;
        }

        private void Start() => 
            _multiplayer.GetReadyHappened += OnGetReadyHappened;

        protected override void Cleanup() => 
            _multiplayer.GetReadyHappened -= OnGetReadyHappened;

        protected override void OnButtonClicked()
        {
            _multiplayer.Leave();
            _gameStateMachine.Enter<LobbyState>();
        }

        private void OnGetReadyHappened() => 
            SetInteractable(false);
    }
}