using Source.Scripts.View.Windows;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.LevelReward
{
    public class LevelRewardWindow : Window
    {
        public RectTransform itemsContainer;
        public Button claimButton;
        public Button closeButton;

        private LevelRewardWindowHandler _handler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _handler = new LevelRewardWindowHandler(this);
            container.Inject(_handler);
            _handler.Init();
        }

        public override void Open()
        {
            _handler.Rescale();
            
            base.Open();
        }
    }
}