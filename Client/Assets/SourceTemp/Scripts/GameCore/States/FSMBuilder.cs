using System;
using System.Collections.Generic;

namespace Source.Scripts.GameCore.States
{
    public class FSMBuilder
    {
        private readonly Dictionary<Type, FSMState> _states;
        private readonly FSM _fsm;

        public FSMBuilder()
        {
            _states = new Dictionary<Type, FSMState>();
            _fsm = new FSM(_states);
        }

        public FSMBuilder Add<TFSMState>(TFSMState state) where TFSMState : FSMState
        {
            Type key = state.GetType();

            if (_states.TryAdd(key, state) == false)
                throw new ArgumentException($"The FSM already contains the state type of {key}");

            return this;
        }

        public FSM Build() => 
            _fsm;
    }
}