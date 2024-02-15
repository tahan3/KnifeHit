using Source.Scripts.Load;
using Source.Scripts.Prefs;
using UnityEngine;

namespace Source.Scripts.Gameplay
{
    public class LevelHandler : ILoader<int>
    {
        public int Level { get; set; }

        public LevelHandler()
        {
            Level = Load();
        }
        
        public int Load()
        {
            return PlayerPrefs.GetInt(PrefsNames.LevelNumber.ToString(), 0);
        }

        public void Clear()
        {
            PlayerPrefs.SetInt(PrefsNames.LevelNumber.ToString(), 0);
        }
        
        public void IncrementLevel()
        {
            PlayerPrefs.SetInt(PrefsNames.LevelNumber.ToString(), ++Level);
        }
    }
}