using Source.Scripts.Load;
using Source.Scripts.Prefs;
using UnityEngine;

namespace Source.Scripts.Gameplay
{
    public class StageHandler : ILoader<int>
    {
        public int Stage { get; set; }
        
        public StageHandler()
        {
            Stage = Load();
        }
        
        public int Load()
        {
            return PlayerPrefs.GetInt(PrefsNames.StageNumber.ToString(), 0);
        }

        public void Clear()
        {
            PlayerPrefs.SetInt(PrefsNames.StageNumber.ToString(), 0);
        }
        
        public void IncrementStage()
        {
            PlayerPrefs.SetInt(PrefsNames.StageNumber.ToString(), ++Stage);
        }
    }
}