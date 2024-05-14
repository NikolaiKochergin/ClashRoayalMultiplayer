using Reflex.Attributes;
using Source.Scripts.UI.Factory;
using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    public class UIRootWorldCameraSetter : MonoBehaviour
    {
        [SerializeField] private Camera _worldCamera;

        [Inject]
        private void Construct(IUIFactory container)
        {
            if(container.UIRoot)
                container.UIRoot.SetCanvas(_worldCamera);
        }
    }
}