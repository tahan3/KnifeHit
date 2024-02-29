using System;
using NaughtyAttributes.Test;
using Source.Extension;
using Source.Scripts.Currency;
using Source.Scripts.DailyReward;
using Source.Scripts.Gameplay.Timer;
using Source.Scripts.View.Animations;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.DailyGift
{
    public class DailyGiftWindowHandler
    {
        private DailyGiftWindow _giftWindow;

        private DateTimer _testDateTimer;

        [Inject] private RewardAnimations _rewardAnimations;
        [Inject] private CurrencyHandler _currencyHandler;
        
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
            /*_testDateTimer = new DateTimer("test", DateTime.Now.AddHours(5).Trim(TimeSpan.TicksPerSecond));
            UpdateDate(_testDateTimer.Time.Value);
            _testDateTimer.Time.OnValueChanged += UpdateDate;
            _testDateTimer.StartTimer();

            _giftWindow.freeDailyGift.button.gameObject.SetActive(false);
            _giftWindow.freeDailyGift.timerText.gameObject.SetActive(true);
            _giftWindow.freeDailyGift.timerBG.gameObject.SetActive(true);*/
            
            int showCoins = 10;
            int reward = 20;
            
            _rewardAnimations.Animate(DailyRewardType.Coin,
                _giftWindow.freeDailyGift.button.image.rectTransform.position,
                () => _currencyHandler.Currencies[CurrencyType.Coin].Counter.Value++, 
                () => _currencyHandler.Currencies[CurrencyType.Coin].Counter.Value+=reward-showCoins,
                showCoins);
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