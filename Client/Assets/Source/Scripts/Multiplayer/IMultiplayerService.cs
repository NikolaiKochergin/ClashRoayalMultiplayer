using System;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.Multiplayer
{
    public interface IMultiplayerService
    {
        event Action GetReadyHappened;
        event Action StartGameHappened;
        event Action CancelStartHappened;
        UniTask Connect();
        UniTask Leave();
    }
}