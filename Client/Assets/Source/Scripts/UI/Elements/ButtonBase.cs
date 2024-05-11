using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.UI.Elements
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] private Button _button;

        protected void AddListener(UnityAction action) =>
            _button.onClick.AddListener(action);

        protected void RemoveListener(UnityAction action) =>
            _button.onClick.RemoveListener(action);

#if UNITY_EDITOR
        private void Reset() => 
            _button = GetComponent<Button>();
#endif
    }
}