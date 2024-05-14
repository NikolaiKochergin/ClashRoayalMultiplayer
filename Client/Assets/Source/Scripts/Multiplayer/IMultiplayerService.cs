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
        IReadOnlyList<int> PlayerCardIDs { get; }
        IReadOnlyList<int> EnemyCardIDs { get; }
        UniTask Connect();
        UniTask Leave();
    }
}