using System.Collections.Generic;
using Newtonsoft.Json;
using Source.Scripts.Counter;
using Source.Scripts.DailyReward;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Save;
using Source.Scripts.View.Animations;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Currency
{
    public class CurrencyHandler : ILoader<Dictionary<CurrencyType, ICounter>>, ISaver
    {
        public Dictionary<CurrencyType, ICounter> Currencies { get; private set; }

        [Inject] private RewardAnimations _rewardAnimations;
        
        public CurrencyHandler()
        {
            Currencies = Load();
        }
        
        public Dictionary<CurrencyType, ICounter> Load()
        {
            var defaultCurrency = new Dictionary<CurrencyType, int>()
            {
                { CurrencyType.Coin, 0 },
                { CurrencyType.Cash, 0 }
            };

            var data = JsonConvert.DeserializeObject<Dictionary<CurrencyType, int>>(
                PlayerPrefs.GetString(PrefsNames.Currency.ToString())) ?? defaultCurrency;

            var currency = new Dictionary<CurrencyType, ICounter>();
            
            foreach (var keyValuePair in data)
            {
                currency.Add(keyValuePair.Key, new PositiveCounter(keyValuePair.Value));
            }
            
            return currency;
        }

        public void AddCurrency(CurrencyType type, Vector2 position, int countToShow, int totalCurrencyToAdd)
        {
            int showCoins = countToShow;
            int reward = totalCurrencyToAdd;
            DailyRewardType rewardType = type == CurrencyType.Coin ? DailyRewardType.Coin : DailyRewardType.Cash;
            
            _rewardAnimations.Animate(rewardType,
                position,
                () => Currencies[type].Counter.Value++, 
                () =>
                {
                    Currencies[type].Counter.Value += reward - showCoins;
                    Save();
                },
                showCoins);
        }
        
        public void Save()
        {
            var dataToSave = new Dictionary<CurrencyType, int>();

            foreach (var keyValuePair in Currencies)
            {
                dataToSave.Add(keyValuePair.Key, keyValuePair.Value.Counter.Value);
            }

            PlayerPrefs.SetString(PrefsNames.Currency.ToString(), JsonConvert.SerializeObject(dataToSave));
        }
    }
}