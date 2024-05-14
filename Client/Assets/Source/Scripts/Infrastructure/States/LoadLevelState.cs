using Cysharp.Threading.Tasks;
using Reflex.Core;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.States.Machine;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly Container _container;
        private readonly SceneLoader _sceneLoader;
        private readonly IGameStateMachine _gameStateMachine;

        public LoadLevelState(Container container, SceneLoader sceneLoader, IGameStateMachine gameStateMachine)
        {
            _container = container;
            _sceneLoader = sceneLoader;
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(string sceneName) =>
            OnEnter(sceneName).Forget();

        public void Exit() { }

        private async UniTaskVoid OnEnter(string sceneName)
        {
            Cleanup();
            await _sceneLoader.LoadAsync(sceneName);
            _gameStateMachine.Enter<BattleState>();
        }

        private void Cleanup()
        {
            foreach (ICleanable cleanable in _container.All<ICleanable>()) 
                cleanable.Cleanup();
        }
    }
}