using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class YourScoreWindow : Window
    {
        public TextMeshProUGUI yourScore;
        public TextMeshProUGUI timeScore;
        public TextMeshProUGUI totalScore;
        public Button okButton;

        private YourScoreHandler _yourScoreHandler;

        public override void Open()
        {
            _yourScoreHandler.Init();
            
            base.Open();
        }

        [Inject]
        public void Construct(DiContainer container)
        {
            _yourScoreHandler = new YourScoreHandler(this);

            container.Inject(_yourScoreHandler);
        }
    }
}