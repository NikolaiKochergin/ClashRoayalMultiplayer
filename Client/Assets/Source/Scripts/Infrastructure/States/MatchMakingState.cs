using Source.Scripts.Infrastructure.States.Machine;
using Source.Scripts.Multiplayer;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Scripts.Infrastructure.States
{
    [Preserve]
    public class MatchMakingState : IState
    {
        private readonly IMultiplayerService _multiplayer;
        private readonly IGameStateMachine _gameStateMachine;

        public MatchMakingState(IMultiplayerService multiplayer, IGameStateMachine gameStateMachine)
        {
            _multiplayer = multiplayer;
            _gameStateMachine = gameStateMachine;
        }
        
        public void Enter()
        {
            Debug.Log(">>>>>>>> Prepare to match");
            _gameStateMachine.Enter<LoadLevelState, string>("BattleScene");
            _multiplayer.Connect();
        }

        public void Exit()
        {
        }
    }
}