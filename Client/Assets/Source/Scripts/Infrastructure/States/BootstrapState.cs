using System.Linq;
using Cysharp.Threading.Tasks;
using Reflex.Core;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Infrastructure.States.Machine;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class BootstrapState : IState
    {
        private readonly Container _container;

        public BootstrapState(Container container) => 
            _container = container;

        public void Enter() => 
            _container.Resolve<SceneLoader>().Load("BootScene", () => OnLoaded().Forget());

        private async UniTaskVoid OnLoaded()
        {
            await _container.Resolve<IStaticDataLoader>().Load();
            
            await UniTask
                .WhenAll(Enumerable
                    .Select(_container
                        .All<IInitializable>(), initializable => initializable
                        .Initialize()));
            
            _container
                .Resolve<IGameStateMachine>()
                .Enter<AuthorizationState>();
        }

        public void Exit(){}
    }
}