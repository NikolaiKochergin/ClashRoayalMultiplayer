using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.GameCore.States
{
    public class FSM
    {
        private readonly Dictionary<Type, FSMState> _states;

        public FSM(Dictionary<Type, FSMState> states) => 
            _states = states;

        public FSMState CurrentState { get; private set; }
        
        public void Set<T>() where T : FSMState
        {
            if (_states.TryGetValue(typeof(T), out FSMState state) == false)
            {
                Debug.LogError($"State {typeof(T)} does not exist.");
                return;
            }
            
            CurrentState?.Exit();
            CurrentState = state;
            CurrentState.Enter();
        }

        public void Update() => 
            CurrentState.Update();
    }
}