using System.Collections.Generic;
using Newtonsoft.Json;
using Source.Scripts.Counter;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.Currency
{
    public class CurrencyHandler : ILoader<Dictionary<CurrencyType, ICounter>>, ISaver
    {
        public Dictionary<CurrencyType, ICounter> Currencies { get; private set; }

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