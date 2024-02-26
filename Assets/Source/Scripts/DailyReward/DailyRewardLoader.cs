using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Source.Scripts.Load;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.DailyReward
{
    public class DailyRewardLoader : ILoader<int>, ISaver
    {
        public int Day { get; private set; }

        private DateTime _lastTime;

        public DailyRewardLoader()
        {
            Load();
        }
        
        public int Load()
        {
            _lastTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(RewardPrefs.Time.ToString(),
                DateTime.MinValue.ToBinary().ToString())));

            Day = PlayerPrefs.GetInt(RewardPrefs.Day.ToString(), -1);

            if (_lastTime <= DateTime.Now.AddDays(-1))
            {
                Day++;

                if (Day > 6)
                {
                    Day = 0;
                }
            }
            
            return Day;
        }
        
        public void Save()
        {
            PlayerPrefs.SetInt(RewardPrefs.Day.ToString(), Day);
            PlayerPrefs.SetString(RewardPrefs.Time.ToString(), DateTime.Now.ToBinary().ToString());
        }
    }
}