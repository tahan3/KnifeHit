using System;
using Cysharp.Threading.Tasks;
using Source.Scripts.Counter;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Gameplay
{
    public class GameOverHandler
    {
        public event Action OnGameOver;

        private readonly int _pointsToWin;
        private readonly float _gameOverDelay;

        public GameOverHandler(ICounter counter, int pointsToWin, float gameOverDelay = 1f)
        {
            _pointsToWin = pointsToWin;
            _gameOverDelay = gameOverDelay;

            InitCounter(counter);
        }

        private void InitCounter(ICounter counter)
        {
            counter.CounterNumber.OnValueChanged += GameOverCheck;
        }

        private void GameOverCheck(int currentPointsNumber)
        {
            if (currentPointsNumber >= _pointsToWin)
            {
                GameOverAction();
            }
        }

        private async void GameOverAction()
        {
            OnGameOver?.Invoke();

            await UniTask.WaitForSeconds(_gameOverDelay);
            
            SceneManager.LoadSceneAsync("MainGameplay");
        }
    }
}