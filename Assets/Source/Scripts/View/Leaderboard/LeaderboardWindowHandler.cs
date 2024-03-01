using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using PlayFab;
using PlayFab.ClientModels;
using Source.Scripts.Data;
using Source.Scripts.Data.Leaderboard;
using Source.Scripts.Data.Profile;
using Source.Scripts.Data.Screen;
using Source.Scripts.Leaderboard;
using Source.Scripts.Login;
using Source.Scripts.Profile;
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
        private LeaderboardItem _playerItem;
        private Action<GetUserDataResult> OnGetItemData;

        [Inject] private LeaderboardData _leaderboardData;
        [Inject] private ILoginHandler<string> _loginHandler;
        [Inject] private IProfileHandler _profileHandler;
        [Inject] private MainConfig _mainConfig;
        [Inject] private WindowsHandler _windowsHandler;

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
            if (PlayerPrefs.HasKey(_mainConfig.missions[0].missionName))
            {
                if (!PlayerPrefs.HasKey(_mainConfig.missions[0].missionName + "Window"))
                {
                    _leaderboardWindow.playerContainer.gameObject.SetActive(false);
                    _leaderboardWindow.button.gameObject.SetActive(true);

                    _leaderboardWindow.button.onClick.AddListener(() =>
                        _windowsHandler.CloseWindow(WindowType.Leaderboard));
                    PlayerPrefs.SetInt(_mainConfig.missions[0].missionName + "Window", 1);
                }
                else
                {
                    _leaderboardWindow.playerContainer.gameObject.SetActive(true);
                    _leaderboardWindow.button.gameObject.SetActive(false);
                }
            }
            
            if (_items.Count > 0)
            {
                foreach (var leaderboardItem in _items.Values)
                {
                    Object.Destroy(leaderboardItem.gameObject);
                }
                
                Object.Destroy(_playerItem);

                _items.Clear();
            }
            
            var request = new GetLeaderboardRequest
            {
                StatisticName = LeaderboardType.Points.ToString(),
                StartPosition = 0,
                MaxResultsCount = 10,
            };

            var playerRequest = new GetLeaderboardAroundPlayerRequest
            {
                StatisticName = LeaderboardType.Points.ToString(),
                MaxResultsCount = 1
            };
            
            PlayFabClientAPI.GetLeaderboard(request, OnSuccessGetLeaderboard, OnErrorGetLeaderboard);
            PlayFabClientAPI.GetLeaderboardAroundPlayer(playerRequest, OnSuccessGetPlayerLeaderboard, OnErrorGetLeaderboard);
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
                item.number.text = (items[i].Position + 1).ToString();
                item.points.text = items[i].StatValue.ToString();

                _items.Add(items[i].PlayFabId, item);
            }

            for (var i = 0; i < items.Count; i++)
            {
                InitLeaderboardItem(items[i]);
            }
        }

        private void OnSuccessGetPlayerLeaderboard(GetLeaderboardAroundPlayerResult obj)
        {
            var items = obj.Leaderboard;

            for (var i = 0; i < items.Count; i++)
            {
                if (items[i].PlayFabId.Equals(_loginHandler.ID))
                {
                    _playerItem = Object.Instantiate(_leaderboardData.playerPlacePrefab, _leaderboardWindow.playerContainer);
                    _playerItem.playerName.text =
                        string.IsNullOrEmpty(items[i].DisplayName) ? "Name" : items[i].DisplayName;
                    _playerItem.number.text = (items[i].Position + 1).ToString();
                    _playerItem.points.text = items[i].StatValue.ToString();
                    
                    if (_leaderboardData.icons.TryGetValue(_profileHandler.Profile.IconID, out var sprite))
                    {
                        _playerItem.icon.sprite = sprite;
                    }
                }
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
                    if (_items.ContainsKey(id))
                    {
                        _items[id].icon.sprite = sprite;
                        _items[id].playerName.text = profileData.Nickname;
                    }
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