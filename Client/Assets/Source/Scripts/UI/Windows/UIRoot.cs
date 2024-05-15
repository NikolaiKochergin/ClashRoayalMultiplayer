using UnityEngine;

namespace Source.Scripts.UI.Windows
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private float _planeDistance = 5;

        public float ScaleFactor => _canvas.scaleFactor;

        public void SetCanvas(Camera worldCamera)
        {
            _canvas.worldCamera = worldCamera;
            _canvas.planeDistance = _planeDistance;
        }
    }
}