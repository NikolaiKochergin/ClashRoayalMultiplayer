using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public void SetCanvas(Camera worldCamera) => 
            _canvas.worldCamera = worldCamera;
    }
}