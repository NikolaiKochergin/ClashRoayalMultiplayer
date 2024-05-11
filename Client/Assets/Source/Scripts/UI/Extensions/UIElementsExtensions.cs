using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.UI.Extensions
{
    public static class UIElementsExtensions
    {
        public static void AddListener(this Button button, UnityAction action) => 
            button.onClick.AddListener(action);

        public static void RemoveListener(this Button button, UnityAction action) => 
            button.onClick.RemoveListener(action);
    }
}