using System.Linq;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.StaticData;

namespace Source.Scripts.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly Container _container;

        public BootstrapState(Container container) => 
            _container = container;

        public void Enter() => 
            _container.Single<SceneLoader>().Load("BootScene", () => OnLoaded().Forget());

        private async UniTaskVoid OnLoaded()
        {
            await _container.Single<IStaticDataLoader>().Load();
            
            await UniTask
                .WhenAll(Enumerable
                    .Select(_container
                        .All<IInitializable>(), initializable => initializable
                        .Initialize()));
            
            _container
                .Single<IGameStateMachine>()
                .Enter<AuthorizationState>();
        }

        public void Exit(){}
    }
}