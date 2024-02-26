using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.DailyReward
{
    [CreateAssetMenu(fileName = "DailyRewardConfig", menuName = "DailyRewardConfig", order = 0)]
    public class DailyRewardConfig : ScriptableObject
    {
        public List<DailyRewardData> dailyRewards;
        public List<DailyRewardData> lastDayRewards;
    }
}