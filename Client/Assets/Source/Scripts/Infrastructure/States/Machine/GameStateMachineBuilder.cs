using System;
using System.Collections.Generic;
using Reflex.Core;

namespace Source.Scripts.Infrastructure.States.Machine
{
    public class GameStateMachineBuilder
    {
        private readonly Container _container;
        private readonly GameStateMachine _gameStateMachine;
        private readonly Dictionary<Type, IExitableState> _states = new();

        public GameStateMachineBuilder(ContainerBuilder builder)
        {
            _gameStateMachine = new GameStateMachine(_states);
            _container = builder
                .AddSingleton(_gameStateMachine, typeof(IGameStateMachine))
                .Build();
        }

        public GameStateMachineBuilder Add<TState>() where TState : IExitableState
        {
            if(_states.TryAdd(typeof(TState), _container.Construct<TState>()) == false)
                throw new ArgumentException($"The GameStateMachine already contains the state type of {typeof(TState)}");
            
            return this;
        }

        public GameStateMachine Build() => 
            _gameStateMachine;
    }
}