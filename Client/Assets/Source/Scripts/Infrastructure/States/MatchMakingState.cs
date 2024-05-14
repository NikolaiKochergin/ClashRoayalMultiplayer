using Cysharp.Threading.Tasks;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Infrastructure.States.Machine;
using Source.Scripts.Multiplayer;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class MatchMakingState : IState
    {
        private readonly IMultiplayerService _multiplayer;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IStaticDataService _staticData;
        private readonly IWindowService _windows;

        public MatchMakingState(
            IMultiplayerService multiplayer,
            IGameStateMachine gameStateMachine,
            IStaticDataService staticData,
            IWindowService windows)
        {
            _multiplayer = multiplayer;
            _gameStateMachine = gameStateMachine;
            _staticData = staticData;
            _windows = windows;
        }
        
        public void Enter()
        {
            _multiplayer.StartGameHappened += OnStartGameHappened;
            _multiplayer.CancelStartHappened += OnCancelStartHappened;
            OnEnter().Forget();
        }

        public void Exit()
        {
            _multiplayer.StartGameHappened -= OnStartGameHappened;
            _multiplayer.CancelStartHappened -= OnCancelStartHappened;
            _windows.CloseWindow(WindowId.MatchMaking);
        }

        private async UniTaskVoid OnEnter()
        {
            await _multiplayer.Connect();
            await _windows.OpenWindow(WindowId.MatchMaking);
        }

        private void OnStartGameHappened()
        {
            _gameStateMachine.Enter<LoadLevelState, string>(_staticData.ForBattleScene());
        }

        private void OnCancelStartHappened() => 
            _gameStateMachine.Enter<LobbyState>();
    }
}