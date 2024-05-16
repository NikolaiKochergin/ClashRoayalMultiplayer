using Reflex.Core;
using Source.Scripts.GameCore.Battle.MapLogic;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    [RequireComponent(typeof(SceneScope))]
    public class SceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MapInfo _mapInfo;
        
        public void InstallBindings(ContainerBuilder builder) => 
            builder.AddSingleton(_mapInfo);
    }
}
