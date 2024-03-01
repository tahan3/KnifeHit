using Newtonsoft.Json;
using Source.Scripts.Currency;
using Source.Scripts.Data.LevelReward;
using Source.Scripts.Level;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.View.LevelReward
{
    public class LevelRewardHandler : ILoader<PlayersLevelInfo>, ISaver
    {
        public PlayersLevelInfo CollectedRewards { get; private set; }

        private readonly LevelRewardConfig _rewards;
        private readonly PlayersLevelInfo _playersLevelInfo;
        private readonly CurrencyHandler _currencyHandler;
        
        public LevelRewardHandler(LevelRewardConfig rewards, PlayersLevelInfo playersLevelInfo, CurrencyHandler currencyHandler)
        {
            _rewards = rewards;
            _playersLevelInfo = playersLevelInfo;
            _currencyHandler = currencyHandler;
            CollectedRewards = Load();
        }

        public void ClaimRewards()
        {
            if (CollectedRewards.level < 0)
            {
                var info = CollectedRewards;
                info.level = 0;
                CollectedRewards = info;
            }


            var rewards = CollectedRewards;
            rewards.level = _playersLevelInfo.level;
            CollectedRewards = rewards;
            
            Save();
        }
        
        public PlayersLevelInfo Load()
        {
            PlayersLevelInfo info;
            
            if (PlayerPrefs.HasKey(PrefsNames.RewardLevelInfo.ToString()))
            {
                info = JsonConvert.DeserializeObject<PlayersLevelInfo>
                    (PlayerPrefs.GetString(PrefsNames.RewardLevelInfo.ToString()));
            }
            else
            {
                info = new PlayersLevelInfo(0, -1);
            }

            return info;
        }

        public void Save()
        {
            PlayerPrefs.SetString(PrefsNames.RewardLevelInfo.ToString(), JsonConvert.SerializeObject(CollectedRewards));
        }
    }
}