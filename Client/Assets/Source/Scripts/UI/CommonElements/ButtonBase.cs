using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Source.Scripts.UI.CommonElements
{
    public abstract class ButtonBase : MonoBehaviour
    {
        [SerializeField] private Button _button;

        private void Awake()
        {
            AddListener(OnButtonClicked);
            Initialize();
        }

        private void OnDestroy()
        {
            Cleanup();
            RemoveListener(OnButtonClicked);
        }

        public void AddListener(UnityAction action) =>
            _button.onClick.AddListener(action);

        public void RemoveListener(UnityAction action) =>
            _button.onClick.RemoveListener(action);

        public void SetInteractable(bool value) => 
            _button.interactable = value;
        
        protected virtual void Initialize() { }
        protected virtual void Cleanup() { }

        protected virtual void OnButtonClicked() { }

#if UNITY_EDITOR
        private void Reset() => 
            _button = GetComponent<Button>();
#endif
    }
}