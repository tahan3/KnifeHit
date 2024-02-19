using Source.Scripts.View.Windows;
using TMPro;
using Zenject;

namespace Source.Scripts.View.Gameplay
{
    public class TimeBonusWindow : Window
    {
        public TextMeshProUGUI seconds;
        public TextMeshProUGUI points;

        private TimeBonusHandler _timeBonusHandler;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            _timeBonusHandler = new TimeBonusHandler(this);
            container.Inject(_timeBonusHandler);
        }

        public override void Open()
        {
            base.Open();

            _timeBonusHandler.Init();
        }
    }
}