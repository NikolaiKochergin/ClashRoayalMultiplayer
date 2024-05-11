using UnityEngine;

namespace Source.Scripts.Extensions
{
    public static class ComponentExtensions
    {
        public static void Activate(this Component component) => 
            component.gameObject.SetActive(true);

        public static void Deactivate(this Component component) => 
            component.gameObject.SetActive(false);

        public static void Destroy(this Component component) => 
            Object.Destroy(component.gameObject);
    }
}