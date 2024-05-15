using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Source.Scripts.Multiplayer
{
    public interface IMultiplayerService
    {
        event Action GetReadyHappened;
        event Action StartGameHappened;
        event Action CancelStartHappened;
        IReadOnlyList<string> PlayerCardIDs { get; }
        IReadOnlyList<string> EnemyCardIDs { get; }
        UniTask Connect();
        UniTask Leave();
    }
}