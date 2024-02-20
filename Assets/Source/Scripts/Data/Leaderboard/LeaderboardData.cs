using Source.Scripts.View.Leaderboard;
using UnityEngine;

namespace Source.Scripts.Data.Leaderboard
{
    [CreateAssetMenu(fileName = "LeaderboardData", menuName = "LeaderboardData", order = 0)]
    public class LeaderboardData : ScriptableObject
    {
        [Header("Profile icons")]
        public KeyValueStorage<int, Sprite> icons;

        [Header("Prefabs")] 
        public LeaderboardItem firstPlacePrefab;
        public LeaderboardItem secondPlacePrefab;
        public LeaderboardItem thirdPlacePrefab;
        public LeaderboardItem anyPlacePrefab;
        public LeaderboardItem playerPlacePrefab;
    }
}