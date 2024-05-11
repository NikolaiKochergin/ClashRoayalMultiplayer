using System;
using System.Collections.Generic;
using Reflex.Core;

namespace Source.Scripts.Infrastructure.States
{
    public class GameStateMachine : IGameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(Container container) =>
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = container.Construct<BootstrapState>(),
                [typeof(AuthorizationState)] = container.Construct<AuthorizationState>(),
                [typeof(LobbyState)] = container.Construct<LobbyState>(),
                [typeof(LoadLevelState)] = container.Construct<LoadLevelState>(),
                [typeof(BattleState)] = container.Construct<BattleState>(),
            };

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
        {
            TState state = ChangeState<TState>();
            state.Enter(payload);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState
        {
            if (_states.TryGetValue(typeof(TState), out IExitableState state) && state is TState typedState)
                return typedState;

            throw new ArgumentException(
                $"State of type {typeof(TState)} not found or does not implement IExitableState.");
        }
    }
}