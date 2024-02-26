using Source.Scripts.Data.UI;
using Source.Scripts.View.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Settings
{
    public class SettingsWindow : PauseWindow
    {
        [SerializeField] private ToggleSpritesStorage soundSprites;
        [SerializeField] private ToggleSpritesStorage vibroSprites;
        
        public Button soundButton;
        public Button vibroButton;
        public Button closeButton;

        private SettingsWindowHandler _settingsWindowHandler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _settingsWindowHandler = new SettingsWindowHandler(this);
            container.Inject(_settingsWindowHandler);
            _settingsWindowHandler.Init();
        }

        public void SetSoundButtonSprites(bool status)
        {
            soundButton.image.sprite = soundSprites.GetSprite(status);
        }
        
        public void SetVibroButtonSprites(bool status)
        {
            vibroButton.image.sprite = vibroSprites.GetSprite(status);
        }
    }
}