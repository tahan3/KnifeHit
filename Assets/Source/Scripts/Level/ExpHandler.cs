using Newtonsoft.Json;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Reactive;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.Level
{
    public class ExpHandler : ILoader<PlayersLevelInfo>, ISaver
    {
        public ReactiveVariable<PlayersLevelInfo> LevelInfo { get; private set; }

        public readonly int ExpToLevelUp = 100;
        
        public ExpHandler()
        {
            LevelInfo = new ReactiveVariable<PlayersLevelInfo>();
            LevelInfo.Value = Load();
        }

        public void GetExp(int exp)
        {
            LevelInfo.Value.exp += exp;

            if (LevelInfo.Value.exp >= ExpToLevelUp)
            {
                LevelInfo.Value.exp %= ExpToLevelUp;
                LevelInfo.Value.level++;
            }

            Save();
        }
        
        public PlayersLevelInfo Load()
        {
            var info = JsonConvert.DeserializeObject<PlayersLevelInfo>
                (PlayerPrefs.GetString(PrefsNames.PlayersLevelInfo.ToString())) ?? new PlayersLevelInfo();

            return info;
        }

        public void Save()
        {
            PlayerPrefs.SetString(PrefsNames.PlayersLevelInfo.ToString(), JsonConvert.SerializeObject(LevelInfo));
        }
    }
}