using Source.Scripts.UI.ProgressBar;
using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class GameplayWindow : Window
    {
        public TextMeshProUGUI timerText;
        public TextMeshProUGUI pointsText;
        public TextMeshProUGUI pointsFactorText;
        public TextMeshProUGUI challengeText;
        public Button pauseButton;
        public Button settingsButton;
        
        [Header("Progress")]
        public StepProgress knifesProgress;
        public StepProgress stageProgress;

        [Inject]
        private void Construct(DiContainer container)
        {
            var gameplayWindowHandler = new GameplayWindowHandler(this);
            container.Inject(gameplayWindowHandler);

            gameplayWindowHandler.Init();
        }
    }
}