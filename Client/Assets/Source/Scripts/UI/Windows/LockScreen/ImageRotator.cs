using UnityEngine;

namespace Source.Scripts.UI.Windows.LockScreen
{
    public class ImageRotator : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update() => 
            transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }
}