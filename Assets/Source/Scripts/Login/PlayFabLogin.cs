using System;
using PlayFab;
using PlayFab.ClientModels;
using Source.Scripts.Data.Profile;
using UnityEngine;

namespace Source.Scripts.Login
{
    public class PlayFabLogin : ILoginHandler
    {
        public event Action OnLogin;
        public event Action OnError;

        public void Login()
        {
            var request = new LoginWithCustomIDRequest
            {
                CustomId = SystemInfo.deviceUniqueIdentifier,
                CreateAccount = true
            };

            PlayFabClientAPI.LoginWithCustomID(request, OnSuccessLogin, OnErrorLogin);
        }

        private void OnSuccessLogin(LoginResult result)
        {
            OnLogin?.Invoke();
            
            var request = new UpdateUserTitleDisplayNameRequest()
            {
                DisplayName = PlayerPrefs.GetString(ProfilePrefs.ProfileName.ToString(), "Name")
            };
            
            PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnSuccessLoginUpdateUserTitleDisplayName, OnErrorLoginUpdateUserTitleDisplayName);
        }

        private void OnSuccessLoginUpdateUserTitleDisplayName(UpdateUserTitleDisplayNameResult obj)
        {
            Debug.Log("Name: " + obj.DisplayName);
        }
        
        private void OnErrorLoginUpdateUserTitleDisplayName(PlayFabError obj)
        {
            Debug.LogError("Error on update user display name");
        }

        private void OnErrorLogin(PlayFabError result)
        {
            OnError?.Invoke();
            
            Debug.LogError("Error on login");
        }
    }
}