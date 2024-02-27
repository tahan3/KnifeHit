using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Counter;
using Source.Scripts.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Gameplay
{
    public class GameOverHandler
    {
        public event Action OnLevelEnded;
        public event Action OnMissionEnded;

        private readonly int _pointsToWin;
        private readonly float _gameOverDelay;

        private readonly MissionsHandler _missionsHandler;
        
        public GameOverHandler(ICounter counter, MissionsHandler missionsHandler, float gameOverDelay = 1f)
        {
            _missionsHandler = missionsHandler;

            _pointsToWin = _missionsHandler.Mission.stages[_missionsHandler.Stage].levels[_missionsHandler.Level]
                .knifesToWin;
            _gameOverDelay = gameOverDelay;

            counter.Counter.OnValueChanged += LevelEndedCheck;
        }

        private void LevelEndedCheck(int currentPointsNumber)
        {
            if (currentPointsNumber >= _pointsToWin)
            {
                LevelEndedAction();
            }
        }

        private async void LevelEndedAction()
        {
            if (_missionsHandler.Stage >= _missionsHandler.Mission.stages.Count - 1 && _missionsHandler.Level >=
                _missionsHandler.Mission.stages[_missionsHandler.Stage].levels.Count - 1)
            {
                _missionsHandler.EndMission();
                OnLevelEnded?.Invoke();
                OnMissionEnded?.Invoke();
            }
            else
            {
                OnLevelEnded?.Invoke();
                _missionsHandler.EndLevel();
            
                await UniTask.WaitForSeconds(_gameOverDelay);
            
                //SceneManager.LoadSceneAsync("MainGameplay_RomaTest");
                //SceneManager.LoadSceneAsync("MainGameplay");
                //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
                SceneLoader.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}