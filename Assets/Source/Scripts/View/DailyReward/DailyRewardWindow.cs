using System.Collections.Generic;
using Source.Scripts.View.Windows;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.DailyReward
{
    public class DailyRewardWindow : Window
    {
        public List<DailyRewardItem> _items;
        public LastDayRewardItem _lastItem;
        public Button closeButton;

        private DailyRewardWindowHandler _handler;
        
        public override void Open()
        {
            _handler.Init();
            base.Open();
        }

        [Inject]
        public void Construct(DiContainer container)
        {
            _handler = new DailyRewardWindowHandler(this);
            container.Inject(_handler);
        }
    }
}