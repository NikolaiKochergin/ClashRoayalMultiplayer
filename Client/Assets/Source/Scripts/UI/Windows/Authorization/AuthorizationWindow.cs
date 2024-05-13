using Reflex.Attributes;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.Infrastructure.States;
using Source.Scripts.Infrastructure.States.Machine;
using Source.Scripts.UI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.Authorization
{
    public sealed class AuthorizationWindow : WindowBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private TMP_InputField _login;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private Button _signInButton;
        
        private IAuthorizationService _authorization;
        private IGameStateMachine _stateMachine;

        [Inject]
        private void Construct(IAuthorizationService authorization, IGameStateMachine stateMachine)
        {
            _authorization = authorization;
            _stateMachine = stateMachine;
        }

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _signInButton.AddListener(OnSignInButtonClicked);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _signInButton.RemoveListener(OnSignInButtonClicked);
        }

        private void OnSignInButtonClicked()
        {
            _canvasGroup.interactable = false;
            _authorization.Authorize(_login.text, _password.text, OnSuccess, OnError);
        }

        private void OnSuccess()
        {
            _infoText.color = Color.green;
            _infoText.SetText($"User is authorizes with id: {_authorization.UserId}");
            _stateMachine.Enter<LobbyState>();
        }

        private void OnError(string errorMessage)
        {
            _infoText.color = Color.red;
            _infoText.SetText(errorMessage);
            _canvasGroup.interactable = true;
        }
    }
}