using Cysharp.Threading.Tasks;
using Source.Scripts.Infrastructure.States.Machine;
using Source.Scripts.Multiplayer;
using Source.Scripts.Multiplayer.Data;
using Source.Scripts.UI.Services.Windows;
using Source.Scripts.UI.Windows;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class MatchMakingState : IState
    {
        private readonly IMultiplayerService _multiplayer;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IWindowService _windows;

        public MatchMakingState(
            IMultiplayerService multiplayer,
            IGameStateMachine gameStateMachine,
            IWindowService windows)
        {
            _multiplayer = multiplayer;
            _gameStateMachine = gameStateMachine;
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
            _windows.OpenWindow(WindowId.MatchMaking);
            await _multiplayer.Connect();
            
        }

        private void OnStartGameHappened()
        {
            _gameStateMachine.Enter<LoadLevelState, string>("BattleScene");
        }

        private void OnCancelStartHappened() => 
            _gameStateMachine.Enter<LobbyState>();
    }
}