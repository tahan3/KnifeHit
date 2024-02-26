using Source.Scripts.Load;
using Source.Scripts.Prefs;
using Source.Scripts.Save;
using UnityEngine;

namespace Source.Scripts.UI.Vibration
{
    public class VibrationHandler : ILoader<bool>, ISaver
    {
        public bool Mute { get; private set; }

        public VibrationHandler()
        {
            Mute = Load();
        }
        
        public void ChangeMuteStatus()
        {
            Mute = !Mute;
        }
        
        public bool Load()
        {
            return PlayerPrefs.GetInt(PrefsNames.Vibration.ToString(), 0) > 0;
        }

        public void Vibrate()
        {
            if (!Mute)
            {
                Handheld.Vibrate();
            }
        }
        
        public void Save()
        {
            PlayerPrefs.SetInt(PrefsNames.Vibration.ToString(), Mute ? 1 : 0);
        }
    }
}