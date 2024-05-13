using Reflex.Attributes;
using Source.Scripts.Infrastructure.Services.Rating;
using TMPro;
using UnityEngine;

namespace Source.Scripts.UI.Windows.Lobby
{
    public class RatingWidget : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        private IRatingService _rating;

        [Inject]
        private void Construct(IRatingService rating) => 
            _rating = rating;

        private void Start()
        {
            SetText(_rating.Value);
            _rating.Updated += OnRatingUpdated;
        }

        private void OnDestroy() => 
            _rating.Updated -= OnRatingUpdated;

        private void OnRatingUpdated() => 
            SetText(_rating.Value);

        private void SetText((string win, string loss) ratingValue) => 
            _text.SetText($"{ratingValue.win} : {ratingValue.loss}");
    }
}