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
        private const string BootSceneName = "BootScene";
        
        private readonly Container _container;

        public BootstrapState(Container container) => 
            _container = container;

        public void Enter() =>
            OnEnter().Forget();

        private async UniTaskVoid OnEnter()
        {
            await UniTask.WhenAll(
                _container.Resolve<SceneLoader>().LoadAsync(BootSceneName),
                _container.Resolve<IStaticDataLoader>().Load());
            
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