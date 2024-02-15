using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Reactive;
using UnityEngine;

namespace Source.Scripts.Counter
{
    public class TotalBonusesCounter : PositiveCounter, ILoader<int>
    {
        public TotalBonusesCounter()
        {
            Load();
        }
        
        public int Load()
        {
            var value = PlayerPrefs.GetInt(PrefsNames.TotalBonuses.ToString(), 0);
            CounterNumber.Value = value;
            
            return value;
        }
    }
}