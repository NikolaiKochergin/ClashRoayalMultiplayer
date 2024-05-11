﻿using Reflex.Attributes;
using Source.Scripts.Infrastructure.States;
using Source.Scripts.UI.CommonElements;

namespace Source.Scripts.UI.Windows.StartGame
{
    public class StartBattleButton : ButtonBase
    {
        private IGameStateMachine _gameStateMachine;

        [Inject]
        private void Construct(IGameStateMachine gameStateMachine) => 
            _gameStateMachine = gameStateMachine;

        protected override void OnButtonClicked() => 
            _gameStateMachine.Enter<LoadLevelState, string>("BattleScene");
    }
}