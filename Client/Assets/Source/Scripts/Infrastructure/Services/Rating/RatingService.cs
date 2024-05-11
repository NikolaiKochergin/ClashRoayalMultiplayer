using System;
using System.Collections.Generic;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Infrastructure.Services.Network;
using Source.Scripts.Infrastructure.Services.StaticData;

namespace Source.Scripts.Infrastructure.Services.Rating
{
    public class RatingService : IRatingService
    {
        private const string UserID = "userID";
        
        private readonly INetworkService _network;
        private readonly IAuthorizationService _authorization;
        private readonly IStaticDataService _staticData;

        public RatingService(INetworkService network, IAuthorizationService authorization, IStaticDataService staticData)
        {
            _network = network;
            _authorization = authorization;
            _staticData = staticData;
        }
        
        public (string win, string loss) Value { get; private set; }

        public event Action Updated;

        public void LoadRating(Action onSuccess = null, Action<string> onError = null)
        {
            if (_authorization.IsAuthorized == false)
            {
                onError?.Invoke("User is not authorized.");
                return;
            }
            
            Dictionary<string, string> data = new()
            {
                { UserID, _authorization.UserId.ToString() },
            };

            _network.SendRequest(_staticData.ForURL().GetRating, data, OnSuccess, onError);
            return;
            
            void OnSuccess(string request)
            {
                string[] result = request.Split('|');
                if (result.Length != 3 || result[0] != "ok")
                {
                    onError?.Invoke($"Server request: {request}");
                    return;
                }

                Value = (result[1], result[2]);

                onSuccess?.Invoke();
                Updated?.Invoke();
            }
        }
    }
}