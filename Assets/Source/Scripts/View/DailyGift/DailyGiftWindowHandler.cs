using System;
using Facebook.Unity;
using NaughtyAttributes.Test;
using Source.Extension;
using Source.Scripts.Currency;
using Source.Scripts.DailyReward;
using Source.Scripts.Data.Screen;
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
        
        private CurrencyHandler _currencyHandler;
        private WindowsHandler _windowsHandler;

        private const string TimerHoursKey = "HoursTimer";
        private const int TimerHoursDelay = 1;

        private DateTime TimerStarted;
        
        public DailyGiftWindowHandler(DailyGiftWindow giftWindow)
        {
            _giftWindow = giftWindow;

            _testDateTimer = new DateTimer(TimerHoursKey + TimerHoursDelay);
        }

        [Inject]
        public void Construct(CurrencyHandler currencyHandler, WindowsHandler windowsHandler)
        {
            _currencyHandler = currencyHandler;
            _windowsHandler = windowsHandler;

            _giftWindow.freeDailyGift.button.onClick.AddListener(EnableTimer);

            if (DateTime.Now >= _testDateTimer.Time.Value)
            {
                _giftWindow.freeDailyGift.button.gameObject.SetActive(true);
            }
            else
            {
                _giftWindow.freeDailyGift.button.gameObject.SetActive(false);
                _giftWindow.freeDailyGift.timerText.gameObject.SetActive(true);
                _giftWindow.freeDailyGift.timerBG.gameObject.SetActive(true);
                
                TimerStarted = DateTime.Now.Trim(TimeSpan.TicksPerSecond);
                
                _testDateTimer.StartTimer();
            }
            
            _giftWindow.adDailyGift.button.onClick.AddListener(ShowError);
        }
        
        public void Enable()
        {
            if (_testDateTimer != null)
            {
                UpdateDate(_testDateTimer.Time.Value);
                _testDateTimer.Time.OnValueChanged += UpdateDate;
            }
        }

        public void Disable()
        {
            if (_testDateTimer != null)
            {
                _testDateTimer.Time.OnValueChanged -= UpdateDate;
            }
        }

        private void ShowError()
        {
            _windowsHandler.OpenWindow(WindowType.Error);
        }
        
        private void EnableTimer()
        {
            TimerStarted = DateTime.Now.Trim(TimeSpan.TicksPerSecond);
            
            _testDateTimer.SetDelay(DateTime.Now.Trim(TimeSpan.TicksPerSecond)./*AddHours*/AddMinutes(TimerHoursDelay));

            _testDateTimer.StartTimer();
            
            UpdateDate(_testDateTimer.Time.Value);
            _testDateTimer.Time.OnValueChanged += UpdateDate;

            _giftWindow.freeDailyGift.button.gameObject.SetActive(false);
            _giftWindow.freeDailyGift.timerText.gameObject.SetActive(true);
            _giftWindow.freeDailyGift.timerBG.gameObject.SetActive(true);
            
            int reward = 20;

            _currencyHandler.AddCurrency(CurrencyType.Coin,
                _giftWindow.freeDailyGift.button.image.rectTransform.position, reward);
        }
        
        private void UpdateDate(DateTime time)
        {
            _giftWindow.freeDailyGift.timerText.text = (time - TimerStarted).ToString(@"hh\:mm\:ss");

            if (TimerStarted >= time)
            {
                _giftWindow.freeDailyGift.button.gameObject.SetActive(true);
                _giftWindow.freeDailyGift.timerText.gameObject.SetActive(false);
                _giftWindow.freeDailyGift.timerBG.gameObject.SetActive(false);
                
                _testDateTimer.StopTimer();
                _testDateTimer.Time.OnValueChanged -= UpdateDate;
            }
        }
    }
}