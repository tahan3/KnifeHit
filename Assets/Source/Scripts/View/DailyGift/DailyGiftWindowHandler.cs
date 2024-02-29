using System;
using NaughtyAttributes.Test;
using Source.Extension;
using Source.Scripts.Gameplay.Timer;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.DailyGift
{
    public class DailyGiftWindowHandler
    {
        private DailyGiftWindow _giftWindow;

        private DateTimer _testDateTimer;
        
        public DailyGiftWindowHandler(DailyGiftWindow giftWindow)
        {
            _giftWindow = giftWindow;

            _giftWindow.freeDailyGift.button.onClick.AddListener(EnableTimer);
        }
        
        public void Enable()
        {
            //_testDateTimer?.StartTimer();
        }

        public void Disable()
        {
            _testDateTimer?.StopTimer();
        }

        private void EnableTimer()
        {
            _testDateTimer = new DateTimer("test", DateTime.Now.AddHours(5).Trim(TimeSpan.TicksPerSecond));
            UpdateDate(_testDateTimer.Time.Value);
            _testDateTimer.Time.OnValueChanged += UpdateDate;
            _testDateTimer.StartTimer();

            _giftWindow.freeDailyGift.button.gameObject.SetActive(false);
            _giftWindow.freeDailyGift.timerText.gameObject.SetActive(true);
            _giftWindow.freeDailyGift.timerBG.gameObject.SetActive(true);
        }

        private void UpdateDate(DateTime time)
        {
            var now = DateTime.Now.Trim(TimeSpan.TicksPerSecond);
            var timing = time - now;

            _giftWindow.freeDailyGift.timerText.text = string.Format("{0:00}:{1:00}:{2:00}", timing.Hours,
                timing.Minutes, timing.Seconds);
        }
    }
}