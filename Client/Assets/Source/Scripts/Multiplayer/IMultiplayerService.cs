using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Multiplayer.Data;

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