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
using Random = UnityEngine.Random;

namespace Source.Scripts.View.Leaderboard
{
    public class LeaderboardWindowHandler
    {
        private LeaderboardWindow _leaderboardWindow;

        private Dictionary<string, LeaderboardItem> _items = new Dictionary<string, LeaderboardItem>();
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
            if (_items.Count > 0)
            {
                foreach (var leaderboardItem in _items.Values)
                {
                    Object.Destroy(leaderboardItem.gameObject);
                }

                _items.Clear();
            }
            
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
                var item = Object.Instantiate(GetLeaderboardItemByIndex(i), _leaderboardWindow.itemsContainer);
                item.playerName.text = string.IsNullOrEmpty(items[i].DisplayName) ? "Name" : items[i].DisplayName;
                item.number.text = (i + 1).ToString();
                item.points.text = items[i].StatValue.ToString();

                _items.Add(items[i].PlayFabId, item);
            }

            for (var i = 0; i < items.Count; i++)
            {
                InitLeaderboardItem(items[i]);
            }
        }

        private LeaderboardItem GetLeaderboardItemByIndex(int index)
        {
            switch (index)
            {
                case 0:
                    return _leaderboardData.firstPlacePrefab;
                case 1:
                    return _leaderboardData.secondPlacePrefab;
                case 2:
                    return _leaderboardData.thirdPlacePrefab;
                default:
                    return _leaderboardData.anyPlacePrefab;
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
            GetUserDataRequest request = (GetUserDataRequest)result.Request;
            string id = request.PlayFabId;
            
            if (result.Data != null && result.Data.TryGetValue(key, out var value))
            {
                ProfileData profileData = JsonConvert.DeserializeObject<ProfileData>(value.Value);
                
                if (_leaderboardData.icons.TryGetValue(profileData.IconID, out var sprite))
                {
                    _items[id].icon.sprite = sprite;
                    _items[id].playerName.text = profileData.Nickname;
                }
            }
            else
            {
                if (_leaderboardData.icons.TryGetValue(Random.Range(0,4), out var sprite))
                {
                    _items[id].icon.sprite = sprite;
                }
            }
        }

        private void OnGetUserDataError(PlayFabError obj)
        {
            Debug.LogError("Can't get players data: " + obj.Error);
        }
    }
}