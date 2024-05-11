using System;

namespace Source.Scripts.Infrastructure.Services.Rating
{
    public interface IRatingService
    {
        (string win, string loss) Value { get; }
        event Action Updated;
        void LoadRating(Action onSuccess = null, Action<string> onError = null);
    }
}