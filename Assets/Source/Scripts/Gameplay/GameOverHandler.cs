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
        
        private int _pointsToWin;

        public GameOverHandler(ICounter counter, int pointsToWin)
        {
            _pointsToWin = pointsToWin;

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

            await UniTask.WaitForSeconds(0.5f);
            
            SceneManager.LoadSceneAsync("MainGameplay");
        }
    }
}