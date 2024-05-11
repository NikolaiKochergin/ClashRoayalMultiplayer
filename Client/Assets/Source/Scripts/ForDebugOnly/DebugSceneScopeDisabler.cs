using Reflex.Core;
using UnityEngine;

namespace Source.Scripts.ForDebugOnly
{
    public class DebugSceneScopeDisabler : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder containerBuilder) => 
            Destroy(gameObject);
    }
}