using Source.Scripts.Counter;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.Gameplay
{
    public class MissionsCounter : PositiveCounter, ILoader<int>, ISaver
    {
        public MissionsCounter()
        {
            Load();
        }
        
        public int Load()
        {
            var value = PlayerPrefs.GetInt(PrefsNames.OpenedMissionsNumber.ToString(), 0);
            Counter.Value = value;
            
            return value;
        }

        public bool TrySaveProgress(MissionConfig config)
        {
            if (PlayerPrefs.HasKey(config.missionName) || config.missionName.Equals("Tutorial")) return false;
            
            PlayerPrefs.SetInt(config.missionName, 1);
            Counter.Value++;
            Save();
            return true;

        }
        
        public void Save()
        {
            PlayerPrefs.SetInt(PrefsNames.OpenedMissionsNumber.ToString(), Counter.Value);
        }
    }
}