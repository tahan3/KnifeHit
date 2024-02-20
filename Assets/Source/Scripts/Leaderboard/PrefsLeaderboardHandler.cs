using UnityEngine;

namespace Source.Scripts.Leaderboard
{
    public class PrefsLeaderboardHandler : ILeaderboardHandler
    {
        public void SetLeaderboard(LeaderboardType type, int stat)
        {
            PlayerPrefs.SetInt(type.ToString(), stat);
        }
    }
}