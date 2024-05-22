using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using Source.Scripts.Multiplayer;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Countdown
{
    public class CountdownWindow : WindowBase
    {
        [SerializeField] private CountdownMessage _countdownMessage;
        
        private IMultiplayerService _multiplayer;

        [Inject]
        private void Construct(IMultiplayerService multiplayer) => 
            _multiplayer = multiplayer;

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _multiplayer.StartTickHappened += _countdownMessage.DisplayTick;
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _multiplayer.StartTickHappened -= _countdownMessage.DisplayTick;
        }

        public override void Close() => 
            DelayedClose().Forget();

        private async UniTaskVoid DelayedClose()
        {
            await UniTask.Delay(1000);
            base.Close();
        }
    }
}