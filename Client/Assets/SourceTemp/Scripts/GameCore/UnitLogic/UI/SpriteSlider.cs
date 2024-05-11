using UnityEngine;

namespace Source.Scripts.GameCore.UnitLogic.UI
{
    public class SpriteSlider : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _background;
        [SerializeField] private SpriteRenderer _fill;

        public void SetFill(float value) => 
            _fill.size = new Vector2(_background.size.x * value, _fill.size.y);
    }
}
