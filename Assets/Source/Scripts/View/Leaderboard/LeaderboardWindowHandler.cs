using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using Source.Scripts.Data;
using Source.Scripts.Data.Leaderboard;
using Source.Scripts.Data.Profile;
using Source.Scripts.Leaderboard;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Source.Scripts.View.Leaderboard
{
    public class LeaderboardWindowHandler
    {
        private LeaderboardWindow _leaderboardWindow;

        private List<LeaderboardItem> _items = new List<LeaderboardItem>();
        private Action<GetUserDataResult> OnGetItemData;

        [Inject] private LeaderboardData _leaderboardData;

        public LeaderboardWindowHandler(LeaderboardWindow leaderboardWindow)
        {
            _leaderboardWindow = leaderboardWindow;
        }

        public void Init()
        {
            GetLeaderboard();
        }

        private void GetLeaderboard()
        {
            var request = new GetLeaderboardRequest
            {
                StatisticName = LeaderboardType.Points.ToString(),
                StartPosition = 0,
                MaxResultsCount = 10,
            };
            
            PlayFabClientAPI.GetLeaderboard(request, OnSuccessGetLeaderboard, OnErrorGetLeaderboard);
        }

        private void OnErrorGetLeaderboard(PlayFabError obj)
        {
            Debug.LogError("Can't get leaderboard info: " + obj.Error);
        }

        private void OnSuccessGetLeaderboard(GetLeaderboardResult obj)
        {
            var items = obj.Leaderboard;
            
            for (var i = 0; i < items.Count; i++)
            {
                InitLeaderboardItem(items[i]);
            }
        }

        private void InitLeaderboardItem(PlayerLeaderboardEntry playerLeaderboard)
        {
            var request = new GetUserDataRequest
            {
                PlayFabId = playerLeaderboard.PlayFabId
            };
            
            PlayFabClientAPI.GetUserData(request, OnGetUserDataSuccess, OnGetUserDataError);
        }

        private void OnGetUserDataSuccess(GetUserDataResult result)
        {
            var key = PlayFabUserData.Profile.ToString();
            
            if (result.Data != null && result.Data.TryGetValue(key, out var value))
            {
                ProfileData profileData = JsonConvert.DeserializeObject<ProfileData>(value.Value);
                var item = Object.Instantiate(_leaderboardData.anyPlacePrefab, _leaderboardWindow.itemsContainer);
                
                if (_leaderboardData.icons.TryGetValue(profileData.IconID, out var sprite))
                {
                    item.icon.sprite = sprite;
                    item.playerName.text = profileData.Nickname;
                }
            }
        }

        private void OnGetUserDataError(PlayFabError obj)
        {
            Debug.LogError("Can't get players data: " + obj.Error);
        }
    }
}