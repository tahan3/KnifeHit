using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Source.Scripts.Currency;
using Source.Scripts.DailyReward;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Reactive;
using Source.Scripts.Save;
using Source.Scripts.View.Animations;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Level
{
    public class ExpHandler : ILoader<PlayersLevelInfo>, ISaver
    {
        public ReactiveVariable<PlayersLevelInfo> LevelInfo { get; private set; }

        public readonly int ExpToLevelUp = 100;

        public bool LevelEarned;

        [Inject] private RewardAnimations _rewardAnimations;
        
        public ExpHandler()
        {
            LevelInfo = new ReactiveVariable<PlayersLevelInfo>();
            LevelInfo.Value = Load();
        }

        public void GetExp(int exp)
        {
            AddExp(exp);
            
            Save();
        }
        
        public PlayersLevelInfo Load()
        {
            PlayersLevelInfo info;

            if (PlayerPrefs.HasKey(PrefsNames.PlayersLevelInfo.ToString()))
            {
                info = JsonConvert.DeserializeObject<PlayersLevelInfo>
                    (PlayerPrefs.GetString(PrefsNames.PlayersLevelInfo.ToString()));
            }
            else
            {
                info = new PlayersLevelInfo(0, -1);
            }

            return info;
        }

        public void Save()
        {
            PlayerPrefs.SetString(PrefsNames.PlayersLevelInfo.ToString(), JsonConvert.SerializeObject(LevelInfo.Value));
        }
        
        public async UniTask AddStars(Vector2 position, int value)
        {
            int max = 10;
            int showCoins = value;
            int reward = 0;

            if (value > max)
            {
                reward = value - max;
                showCoins = max;
            }

            await _rewardAnimations.Animate(DailyRewardType.Exp,
                position,
                () => AddExp(1),
                () =>
                {
                    AddExp(reward);
                },
                showCoins);
            
            Save();
        }

        private void AddExp(int exp)
        {
            var info = LevelInfo.Value;
            info.exp += exp;
            LevelEarned = false;

            if (info.exp >= ExpToLevelUp)
            {
                info.exp %= ExpToLevelUp;
                info.level++;
                LevelEarned = true;
            }

            LevelInfo.Value = info;
        }
    }
}