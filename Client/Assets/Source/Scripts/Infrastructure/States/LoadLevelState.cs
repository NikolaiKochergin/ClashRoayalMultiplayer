using Reflex.Core;

namespace Source.Scripts.Infrastructure.States
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private readonly Container _container;

        public LoadLevelState(Container container) => 
            _container = container;

        public void Enter(string sceneName) => 
            _container.Resolve<SceneLoader>().Load(sceneName, OnLoaded);

        public void Exit() { }

        private void OnLoaded() => 
            _container.Resolve<IGameStateMachine>().Enter<BattleState>();
    }
}