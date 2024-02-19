using System;
using Source.Scripts.Counter;
using Source.Scripts.Data.Screen;
using Source.Scripts.Gameplay;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class GameplayWindowHandler
    {
        private GameplayWindow _gameplayWindow;
        
        [Inject] private MissionsHandler _missionsHandler;
        [Inject] private KnifesPerRoundCounter _knifesPerRoundCounter;
        [Inject] private MultiplierHandler _multiplierHandler;
        [Inject] private WindowsHandler _windowsHandler;
        
        public GameplayWindowHandler(GameplayWindow gameplayWindow)
        {
            _gameplayWindow = gameplayWindow;
        }

        public void Init()
        {
            _gameplayWindow.settingsButton.onClick.AddListener(ShowSettings);
            
            _gameplayWindow.challengeText.text = "Challenge " + (_missionsHandler.Stage + 1).ToString();
            
            _knifesPerRoundCounter.CounterNumber.OnValueChanged += _gameplayWindow.knifesProgress.SetProgress;

            OnTimerTick(_missionsHandler.Timer.Time.Value);
            _missionsHandler.Timer.Time.OnValueChanged += OnTimerTick;

            OnMultiplierChange(_multiplierHandler.Multiplier.Value);
            _multiplierHandler.Multiplier.OnValueChanged += OnMultiplierChange;

            OnPointsChanged(_missionsHandler.PointsCounter.CounterNumber.Value);
            _missionsHandler.PointsCounter.CounterNumber.OnValueChanged += OnPointsChanged;
        }

        private void ShowSettings()
        {
            _windowsHandler.OpenWindow(WindowType.Settings, true);
        }
        
        private void OnPointsChanged(int points)
        {
            _gameplayWindow.pointsText.text = points.ToString();
        }
        
        private void OnMultiplierChange(float multiplier)
        {
            _gameplayWindow.pointsFactorText.text = multiplier.ToString("X#.##");
        }
        
        private void OnTimerTick(int timerValue)
        {
            TimeSpan time = TimeSpan.FromSeconds(timerValue);
            
            _gameplayWindow.timerText.text = time.ToString(@"mm\:ss");
        }
    }
}