using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Source.Scripts.Infrastructure;
using Source.Scripts.Infrastructure.States;
using Source.Scripts.Infrastructure.States.Machine;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Battle
{
    public class BattleWindow : WindowBase
    {
        private SceneLoader _sceneLoader;
        private IGameStateMachine _gameStateMachine;


        [Inject]
        private void Construct(SceneLoader sceneLoader, IGameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                GoToLobby().Forget();
            }
        }

        private async UniTaskVoid GoToLobby()
        {
            await _sceneLoader.LoadAsync("BootScene");
            _gameStateMachine.Enter<LobbyState>();
        }
    }
}