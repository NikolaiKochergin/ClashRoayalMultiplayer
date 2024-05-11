using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private void Awake() => 
            OnAwake();

        private void Start()
        {
            Initialize();
            SubscribeUpdates();
        }

        private void OnDestroy() => 
            Cleanup();

        public virtual void Close() => 
            Destroy(gameObject);

        protected virtual void OnAwake(){}
        protected virtual void Initialize(){}
        protected virtual void SubscribeUpdates(){}
        protected virtual void Cleanup(){}
    }
}