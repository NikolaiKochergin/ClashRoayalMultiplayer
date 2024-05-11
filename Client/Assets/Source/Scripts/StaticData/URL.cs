using System;
using UnityEngine;

namespace Source.Scripts.StaticData
{
    [Serializable]
    public class URL
    {
        [SerializeField] private string _mainUrl = "http://147.45.146.243/";
        [SerializeField] private string _authorization = "Authorization/authorization.php";
        [SerializeField] private string _registration = "Authorization/registration.php";
        [SerializeField] private string _getDeckInfo = "Game/getDeckInfo.php";
        [SerializeField] private string _updateDeckInfo = "Game/updateSelected.php";
        [SerializeField] private string _getRating = "Game/getRating.php";

        public string Authorization => _mainUrl + _authorization;
        public string Registration => _mainUrl + _registration;
        public string GetDeck => _mainUrl + _getDeckInfo;
        public string UpdateDeck => _mainUrl + _updateDeckInfo;
        public string GetRating => _mainUrl + _getRating;
    }
}