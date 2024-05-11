using Reflex.Core;
using Source.Scripts.GameCore.MapLogic;
using UnityEngine;

namespace Source.Scripts.Infrastructure
{
    public class SceneInstaller : MonoBehaviour, IInstaller
    {
        [SerializeField] private MapInfo _mapInfo;
        
        public void InstallBindings(ContainerBuilder builder) => 
            builder.AddSingleton(_mapInfo);
    }
}
