using Source.Scripts.Gameplay;
using Source.Scripts.Scene;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class TimeIsUpWindowHandler
    {
        private TimeIsUpWindow _window;

        [Inject] private MissionsHandler _missionsHandler;
        
        public TimeIsUpWindowHandler(TimeIsUpWindow window)
        {
            _window = window;
        }

        private void OnEndTime(int time)
        {
            if (time <= 0)
            {
                _window.Open();
            }
        }
        
        public void Init()
        {
            _missionsHandler.Timer.Time.OnValueChanged += OnEndTime;
            _window.restartButton.onClick.AddListener(RestartMission);
            _window.mainMenuButton.onClick.AddListener(BackToMenu);
        }

        private void RestartMission()
        {
            _missionsHandler.LoadMission(_missionsHandler.Mission);
        }

        private void BackToMenu()
        {
            SceneLoader.LoadScene("MainMenu");
        }
    }
}