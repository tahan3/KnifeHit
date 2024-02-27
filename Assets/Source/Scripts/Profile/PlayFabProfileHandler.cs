using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Source.Scripts.Connection;
using Source.Scripts.Data;
using Source.Scripts.Data.Profile;
using Source.Scripts.Load;
using UnityEngine;

namespace Source.Scripts.Profile
{
    public class PlayFabProfileHandler : IProfileHandler
    {
        public ProfileData Profile { get; set; }

        public PlayFabProfileHandler()
        {
            Profile = Load();
        }
        
        public ProfileData Load()
        {
            //PlayFabSaveProfile();

            Profile = new ProfileLoader().Load();
            return Profile;
        }

        public void Save()
        {
            PlayerPrefs.SetString(ProfilePrefs.ProfileName.ToString(), Profile.Nickname);
            PlayerPrefs.SetInt(ProfilePrefs.ProfileIconID.ToString(), Profile.IconID);
            
            PlayFabSaveProfile();
        }

        private void PlayFabSaveProfile()
        {
            if (!InternetConnection.ConnectionCheck()) return;

            var request = new UpdateUserDataRequest
            {
                Data = new Dictionary<string, string>
                {
                    { PlayFabUserData.Profile.ToString(), Newtonsoft.Json.JsonConvert.SerializeObject(Profile) }
                }
            };

            var titleDisplayNameRequest = new UpdateUserTitleDisplayNameRequest()
            {
                DisplayName = Profile.Nickname
            };
            
            PlayFabClientAPI.UpdateUserTitleDisplayName(titleDisplayNameRequest, OnSuccessLoginUpdateUserTitleDisplayName, OnErrorLoginUpdateUserTitleDisplayName);
            PlayFabClientAPI.UpdateUserData(request, OnUdateSuccess, OnUpdateError);
        }

        private void OnSuccessLoginUpdateUserTitleDisplayName(UpdateUserTitleDisplayNameResult obj)
        {
            Debug.Log("Name: " + obj.DisplayName);
        }
        
        private void OnErrorLoginUpdateUserTitleDisplayName(PlayFabError obj)
        {
            Debug.LogError("Error on update user display name");
        }
        
        private void OnUdateSuccess(UpdateUserDataResult obj)
        {
            Debug.Log("Player profile updated");
        }

        private void OnUpdateError(PlayFabError obj)
        {
            Debug.LogError("Player profile update error: " + obj.Error);
        }
    }
}