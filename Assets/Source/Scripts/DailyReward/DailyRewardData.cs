using System;

namespace Source.Scripts.DailyReward
{
    [Serializable]
    public struct DailyRewardData
    {
        public DailyRewardType rewardType;
        public int amount;
    }
}