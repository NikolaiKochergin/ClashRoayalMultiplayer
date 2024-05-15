using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Source.Scripts.GameCore.Deck.StaticData;
using UnityEngine;

namespace Source.Scripts.GameCore.Battle.Services.Player
{
    public interface IPlayerService
    {
        IReadOnlyTeam Team { get; }
        IReadOnlyList<CardInfo> InHandCards { get; }
        CardInfo NextCard { get; }
        void Initialize(IReadOnlyTeam enemyTeam);
        UniTask SpawnUnit(CardInfo card, Vector3 position);
        event Action NextCardUpdated;
    }
}