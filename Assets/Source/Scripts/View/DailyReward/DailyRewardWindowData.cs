using Source.Scripts.DailyReward;
using Source.Scripts.Data;
using Source.Scripts.Data.UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.View.DailyReward
{
    [CreateAssetMenu(fileName = "DailyRewardWindowData", menuName = "DailyRewardWindowData", order = 0)]
    public class DailyRewardWindowData : ScriptableObject
    { 
        public DailyRewardConfig dailyRewardConfig;
        public ToggleSpritesStorage todaySprites;
        public KeyValueStorage<DailyRewardType, Sprite> rewardSprites;
    }
}