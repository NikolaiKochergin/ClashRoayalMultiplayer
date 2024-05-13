using Reflex.Core;

namespace Source.Scripts.Infrastructure.States.Machine
{
    public static class ReflexContainerExtensions
    {
        public static GameStateMachineBuilder AddGameStateMachine(this Container container) => 
            new(container);
    }
}