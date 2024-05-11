using Reflex.Attributes;
using Source.Scripts.Infrastructure.Services.Authorization;
using Source.Scripts.UI.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI.Windows.Registration
{
    public sealed class RegistrationWindow : WindowBase
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private TextMeshProUGUI _infoText;
        [SerializeField] private TMP_InputField _login;
        [SerializeField] private TMP_InputField _password;
        [SerializeField] private TMP_InputField _confirmPassword;
        [SerializeField] private Button _signUpButton;
        
        private IAuthorizationService _authorization;

        [Inject]
        private void Construct(IAuthorizationService authorization) => 
            _authorization = authorization;

        protected override void SubscribeUpdates()
        {
            base.SubscribeUpdates();
            _signUpButton.AddListener(OnSignUpButtonClicked);
        }

        protected override void Cleanup()
        {
            base.Cleanup();
            _signUpButton.RemoveListener(OnSignUpButtonClicked);
        }

        private void OnSignUpButtonClicked()
        {
            _canvasGroup.interactable = false;
            _authorization.Register(_login.text, _password.text, _confirmPassword.text, OnSuccess, OnError);
        }

        private void OnSuccess()
        {
            _infoText.color = Color.green;
            _infoText.SetText("Registration successful");
            _canvasGroup.interactable = true;
        }

        private void OnError(string errorMessage)
        {
            _infoText.color = Color.red;
            _infoText.SetText(errorMessage);
            _canvasGroup.interactable = true;
        }
    }
}