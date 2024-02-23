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

        [Inject]
        public void Construct(DiContainer container)
        {
            var handler = new LevelRewardWindowHandler(this);
            container.Inject(handler);
            handler.Init();
        }
    }
}