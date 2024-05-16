using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Infrastructure.Services.Cameras
{
    [RequireComponent(typeof(Camera))]
    public class MainCamera : MonoBehaviour
    {
        [SerializeField] private Camera _camera;

        [Inject]
        private void Construct(ICameraService camera) => 
            camera.Main = _camera;

#if UNITY_EDITOR
        private void Reset() => 
            _camera = GetComponent<Camera>();
#endif
    }
}