﻿using Reflex.Core;
using Source.Scripts.GameCore.Battle.Services.Enemy;
using Source.Scripts.GameCore.Battle.Services.Player;
using Source.Scripts.GameCore.Deck.Service;
using Source.Scripts.Infrastructure.Services;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Infrastructure.Services.Input;
using Source.Scripts.Infrastructure.Services.Network;
using Source.Scripts.Infrastructure.Services.Rating;
using Source.Scripts.Infrastructure.Services.StaticData;
using Source.Scripts.Infrastructure.States;
using Source.Scripts.UI.Factory;
using Source.Scripts.UI.Services.Windows;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder
                .AddSingleton(typeof(SceneLoader))
                .AddSingleton(typeof(GameStateMachine), typeof(IGameStateMachine))
                .AddSingleton(typeof(StaticDataService), typeof(IStaticDataService), typeof(IStaticDataLoader))
                .AddSingleton(typeof(UIFactory), typeof(IUIFactory), typeof(IInitializable))
                .AddSingleton(typeof(InputService), typeof(IInputService), typeof(IInitializable))
                .AddSingleton(typeof(WindowService), typeof(IWindowService))
                .AddSingleton(typeof(NetworkService), typeof(INetworkService))
                .AddSingleton(typeof(AuthorizationService), typeof(IAuthorizationService))
                .AddSingleton(typeof(DeckService), typeof(IDeckService))
                .AddSingleton(typeof(RatingService), typeof(IRatingService))
                .AddSingleton(typeof(PlayerService), typeof(IPlayerService))
                .AddSingleton(typeof(EnemyService), typeof(IEnemyService))
                .Build()
                .Resolve<IGameStateMachine>()
                .Enter<BootstrapState>();
    }
}