using Source.Scripts.Gameplay;
using Source.Scripts.Scene;
using UnityEngine.Networking;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class GamePauseWindowHandler
    {
        private GamePauseWindow _gamePauseWindow;

        [Inject] private MissionsHandler _missionsHandler;

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

        private void Restart()
        {
            _missionsHandler.LoadMission(_missionsHandler.Mission);
        }
        
        private void Exit()
        {
            _missionsHandler.RestartWave();
            SceneLoader.LoadScene("MainGameplay");
        }
    }
}