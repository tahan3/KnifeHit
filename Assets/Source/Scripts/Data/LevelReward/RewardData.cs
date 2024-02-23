using System;
using Source.Scripts.Currency;

namespace Source.Scripts.Data.LevelReward
{
    [Serializable]
    public struct RewardData
    {
        public CurrencyType currency;
        public int amount;
    }
}