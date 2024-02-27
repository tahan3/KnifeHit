using System;
using System.Collections.Generic;
using Source.Scripts.Currency;
using Source.Scripts.DailyReward;
using Source.Scripts.Data.Screen;
using Source.Scripts.Level;
using Source.Scripts.Prefs;
using Source.Scripts.UI.ProgressBar;
using Source.Scripts.View.Buttons;
using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View
{
    public class MainMenuMainWindow : Window
    {
        [Header("Bottom switches")]
        public Switch shopButton;
        public Switch leaderboardButton;
        public Switch homeButton;
        public Switch dailyRewardButton;
        public Switch profileButton;
        
        [Header("Buttons")]
        public Button settingsButton;
        public Button usdPlusButton;
        public Button expProgressButton;

        [Header("ExpProgressBar")] 
        public ProgressBar<float> expProgress;
        public TextMeshProUGUI levelText;

        [Header("Currency")] 
        public TextMeshProUGUI coins;
        public TextMeshProUGUI cash;
        
        private WindowsHandler _windowsHandler;
        private ExpHandler _expHandler;
        
        [Inject]
        public void Construct(WindowsHandler windowsHandler, ExpHandler expHandler, CurrencyHandler currencyHandler)
        {
            _windowsHandler = windowsHandler;
            _expHandler = expHandler;
            
            leaderboardButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.Leaderboard, true));
            shopButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.Shop, true));
            homeButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.Missions, true));
            dailyRewardButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.DailyGift, true));
            settingsButton.onClick.AddListener(() => windowsHandler.OpenWindow(WindowType.Settings));
            profileButton.button.onClick.AddListener(() => windowsHandler.OpenWindow(WindowType.Profile, true));

            var handler = new SwitchesHandler(new List<Switch>
                { shopButton, leaderboardButton, homeButton, dailyRewardButton, profileButton });

            usdPlusButton.onClick.AddListener(shopButton.button.onClick.Invoke);
            expProgressButton.onClick.AddListener(() => windowsHandler.OpenWindow(WindowType.LevelReward, true));

            expProgress.SetProgress(expHandler.LevelInfo.Value.exp / (float)expHandler.ExpToLevelUp);
            levelText.text = (expHandler.LevelInfo.Value.level + 1).ToString();
            
            ChangeCoins(currencyHandler.Currencies[CurrencyType.Coin].Counter.Value);
            currencyHandler.Currencies[CurrencyType.Coin].Counter.OnValueChanged += ChangeCoins;
            
            ChangeCash(currencyHandler.Currencies[CurrencyType.Cash].Counter.Value);
            currencyHandler.Currencies[CurrencyType.Cash].Counter.OnValueChanged += ChangeCash;
        }

        private void Start()
        {
            homeButton.button.onClick?.Invoke();

            if (!PlayerPrefs.HasKey(PrefsNames.TutorStage.ToString()))
            {
                DateTime lastTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(
                    RewardPrefs.Time.ToString(),
                    DateTime.MinValue.ToBinary().ToString())));

                if (lastTime <= DateTime.Now.AddDays(-1))
                {
                    _windowsHandler.OpenWindow(WindowType.DailyReward, true);
                }
            }
        }
        
        private void ChangeCash(int value)
        {
            cash.text = CurrencyConverter.Convert(CurrencyType.Cash, value);
        }

        private void ChangeCoins(int value)
        {
            coins.text = CurrencyConverter.Convert(CurrencyType.Coin, value);
        }
    }
}