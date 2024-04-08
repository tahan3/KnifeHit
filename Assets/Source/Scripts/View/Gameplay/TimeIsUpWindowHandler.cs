using Source.Scripts.Gameplay;
using Source.Scripts.SceneManagement;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class TimeIsUpWindowHandler
    {
        private TimeIsUpWindow _window;

        [Inject] private MissionsHandler _missionsHandler;
        [Inject] private SceneLoader _sceneLoader;
        
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

        public void OnDisable()
        {
            _missionsHandler.Timer.Time.OnValueChanged -= OnEndTime;
        }
        
        private async void RestartMission()
        {
            _window.restartButton.interactable = false;
            
            await _missionsHandler.LoadMission(_missionsHandler.Mission);

            _window.restartButton.interactable = true;
        }

        private async void BackToMenu()
        {
            _window.mainMenuButton.interactable = false;
            
            await _sceneLoader.LoadScene(SceneType.MainMenu);

            _window.mainMenuButton.interactable = true;
        }
    }
}