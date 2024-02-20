using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using Source.Scripts.Connection;
using Unity.VisualScripting;
using UnityEngine;

namespace Source.Scripts.Leaderboard
{
    public class PlayFabLeaderboardHandler : ILeaderboardHandler
    {
        public void SetLeaderboard(LeaderboardType type, int stat)
        {
            if (!InternetConnection.ConnectionCheck()) return;
            
            var request = new UpdatePlayerStatisticsRequest
            {
                Statistics = new List<StatisticUpdate>
                {
                    new StatisticUpdate
                    {
                        StatisticName = type.ToString(),
                        Value = stat
                    }
                }
            };
            
            PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdateSuccess, OnLeaderBoardUpdateError);
        }

        private void OnLeaderboardUpdateSuccess(UpdatePlayerStatisticsResult obj)
        {
            Debug.Log("Leaderboard updated");
        }
        
        private void OnLeaderBoardUpdateError(PlayFabError obj)
        {
            Debug.LogError("OnLeaderBoardUpdateError: " + obj.Error);
        }
    }
}