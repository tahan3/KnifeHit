using Source.Scripts.Gameplay;
using Source.Scripts.SceneManagement;
using UnityEngine.Networking;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class GamePauseWindowHandler
    {
        private GamePauseWindow _gamePauseWindow;

        [Inject] private MissionsHandler _missionsHandler;
        [Inject] private SceneLoader _sceneLoader;

        public GamePauseWindowHandler(GamePauseWindow gamePauseWindow)
        {
            _gamePauseWindow = gamePauseWindow;
        }

        public void Init()
        {
            _gamePauseWindow.resumeButton.onClick.AddListener(_gamePauseWindow.Close);
            _gamePauseWindow.restartButton.onClick.AddListener(Restart);
            _gamePauseWindow.exitButton.onClick.AddListener(Exit);
        }

        private async void Restart()
        {
            _gamePauseWindow.restartButton.interactable = false;
            
            await _missionsHandler.RestartWave();

            _gamePauseWindow.restartButton.interactable = true;
        }
        
        private async void Exit()
        {
            _gamePauseWindow.exitButton.interactable = false;
            
            await _sceneLoader.LoadScene(SceneType.MainMenu);

            _gamePauseWindow.exitButton.interactable = true;
        }
    }
}