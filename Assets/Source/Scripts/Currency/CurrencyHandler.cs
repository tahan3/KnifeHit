using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Source.Scripts.Counter;
using Source.Scripts.DailyReward;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Save;
using Source.Scripts.View.Animations;
using UnityEngine;
using UnityEngine.SceneManagement;
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

        public async UniTask AddCurrency(CurrencyType type, Vector2 position, int value)
        {
            int max = 10;
            int showCoins = value;
            int reward = 0;

            if (value > max)
            {
                reward = value - max;
                showCoins = max;
            }

            await _rewardAnimations.Animate(type == CurrencyType.Coin ? DailyRewardType.Coin : DailyRewardType.Cash,
                position,
                () => Currencies[type].Counter.Value++,
                () =>
                {
                    Currencies[type].Counter.Value += reward;
                },
                showCoins);
            
            Save();
        }

        public void SelfClean()
        {
            var newCurrencies = Currencies.Keys.ToDictionary<CurrencyType, CurrencyType, ICounter>(key => key,
                key => new PositiveCounter(Currencies[key].Counter.Value));

            Currencies = newCurrencies;
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