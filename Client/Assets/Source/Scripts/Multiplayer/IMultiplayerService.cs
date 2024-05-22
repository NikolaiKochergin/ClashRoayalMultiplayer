using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.Multiplayer.Data;

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
        event Action<TickData> StartTickHappened;
    }
}