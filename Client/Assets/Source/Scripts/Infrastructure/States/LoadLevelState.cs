using Source.Scripts.Infrastructure.States.Machine;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;

        public LoadLevelState(SceneLoader sceneLoader, IGameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter(string sceneName) => 
            _sceneLoader.Load(sceneName, OnLoaded);

        public void Exit() { }

        private void OnLoaded() => 
            _gameStateMachine.Enter<BattleState>();
    }
}