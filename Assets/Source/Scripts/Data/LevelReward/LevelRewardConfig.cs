using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Data.LevelReward
{
    [CreateAssetMenu(fileName = "LevelRewardConfig", menuName = "LevelRewardConfig", order = 0)]
    public class LevelRewardConfig : ScriptableObject
    {
        public List<RewardData> rewardPerLevel;
    }
}