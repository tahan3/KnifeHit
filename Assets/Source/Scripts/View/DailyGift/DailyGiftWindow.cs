using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.DailyGift
{
    public class DailyGiftWindow : Window
    {
        public DailyGiftItem freeDailyGift;
        public DailyGiftItem adDailyGift;

        private DailyGiftWindowHandler _handler;
        
        [Inject]
        public void Construct(DiContainer container)
        {
            _handler = new DailyGiftWindowHandler(this);
            container.Inject(_handler);
        }

        public override void Open()
        {
            _handler.Enable();
            
            base.Open();
        }

        public override void Close()
        {
            _handler.Disable();
            
            base.Close();
        }
    }
}