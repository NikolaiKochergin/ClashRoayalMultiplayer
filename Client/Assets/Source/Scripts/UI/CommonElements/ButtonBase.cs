using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.UI.CommonElements
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake() => 
            AddListener(OnButtonClicked);

        private void OnDestroy() => 
            RemoveListener(OnButtonClicked);

        public void AddListener(UnityAction action) =>
            _button.onClick.AddListener(action);

        public void RemoveListener(UnityAction action) =>
            _button.onClick.RemoveListener(action);

        protected virtual void OnButtonClicked() { }

#if UNITY_EDITOR
        private void Reset() => 
            _button = GetComponent<Button>();
#endif
    }
}