using Source.Scripts.Data.Screen;
using Source.Scripts.Sounds;
using Source.Scripts.UI.Vibration;
using Zenject;

namespace Source.Scripts.View.Settings
{
    public class SettingsWindowHandler
    {
        private SettingsWindow _settingsWindow;

        [Inject] private SoundsHandler _soundsHandler;
        [Inject] private VibrationHandler _vibrationHandler;
        [Inject] private WindowsHandler _windowsHandler;
        
        public SettingsWindowHandler(SettingsWindow settingsWindow)
        {
            _settingsWindow = settingsWindow;
        }

        public void Init()
        {
            _settingsWindow.SetSoundButtonSprites(!_soundsHandler.Mute);
            _settingsWindow.SetVibroButtonSprites(!_vibrationHandler.Mute);
            
            _settingsWindow.soundButton.onClick.AddListener(OnSoundButtonClick);
            _settingsWindow.vibroButton.onClick.AddListener(OnVibroButtonClick);

            _settingsWindow.closeButton.onClick.AddListener(CloseButtonClick);
        }

        private void OnSoundButtonClick()
        {
            _soundsHandler.ChangeMuteStatus();
            _settingsWindow.SetSoundButtonSprites(!_soundsHandler.Mute);
        }

        private void OnVibroButtonClick()
        {
            _vibrationHandler.ChangeMuteStatus();
            _settingsWindow.SetVibroButtonSprites(!_vibrationHandler.Mute);
        }

        private void CloseButtonClick()
        {
            _windowsHandler.CloseWindow(WindowType.Settings);
            
            _soundsHandler.Save();
            _vibrationHandler.Save();
        }
    }
}