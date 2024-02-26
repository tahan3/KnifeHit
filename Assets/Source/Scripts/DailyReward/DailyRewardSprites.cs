using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.DailyReward
{
    [CreateAssetMenu(fileName = "DailyRewardSprites", menuName = "DailyRewardSprites", order = 0)]
    public class DailyRewardSprites : KeyValueStorage<DailyRewardType, Sprite>
    {
    }
}