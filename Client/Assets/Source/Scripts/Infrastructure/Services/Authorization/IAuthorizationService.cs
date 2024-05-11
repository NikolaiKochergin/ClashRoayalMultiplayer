using System;

namespace Source.Scripts.Infrastructure.Services.Authorization
{
    public interface IAuthorizationService
    {
        void Authorize(string login, string password, Action onSuccessCallback = null, Action<string> onErrorCallback = null);
        void Register(string login, string password, string confirmPassword, Action onSuccessCallback = null, Action<string> onErrorCallback = null);
        bool IsAuthorized { get; }
        int UserId { get; }
        event Action AuthorizationHappened;
    }
}