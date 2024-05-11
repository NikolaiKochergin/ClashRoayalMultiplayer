using Reflex.Core;
using Source.Scripts.GameCore.Services.Enemy;
using Source.Scripts.GameCore.Services.Player;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class GameInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder) =>
            builder
                .AddSingleton(typeof(Game))
                .AddSingleton(typeof(PlayerService), typeof(IPlayerService))
                .AddSingleton(typeof(EnemyService), typeof(IEnemyService));
    }
}