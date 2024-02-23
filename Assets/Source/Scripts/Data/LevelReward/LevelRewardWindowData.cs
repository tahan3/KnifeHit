using Source.Scripts.Currency;
using Source.Scripts.Data.UI;
using Source.Scripts.View.LevelReward;
using UnityEngine;

namespace Source.Scripts.Data.LevelReward
{
    [CreateAssetMenu(fileName = "LevelRewardWindowData", menuName = "LevelRewardWindowData", order = 0)]
    public class LevelRewardWindowData : ScriptableObject
    {
        [Header("Prefabs")]
        public LevelRewardProgressBar firstItem;
        public LevelRewardProgressBar midItem;
        public LevelRewardProgressBar lastItem;

        [Header("Sprites")] 
        public ToggleSpritesStorage claimButtonSprites;
        public KeyValueStorage<CurrencyType, Sprite> rewardSprites;
    }
}