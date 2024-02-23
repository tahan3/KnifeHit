using System.Collections.Generic;
using Source.Scripts.Currency;
using Source.Scripts.Data.Screen;
using Source.Scripts.Level;
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

        [Header("Currency")] 
        public TextMeshProUGUI coins;
        public TextMeshProUGUI cash;
        
        private CurrencyHandler _currencyHandler;
        
        [Inject]
        public void Construct(WindowsHandler windowsHandler, ExpHandler expHandler, CurrencyHandler currencyHandler)
        {
            leaderboardButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.Leaderboard, true));
            shopButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.Shop, true));
            homeButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.Missions, true));
            dailyRewardButton.button.onClick.AddListener(()=>windowsHandler.OpenWindow(WindowType.DailyReward, true));
            settingsButton.onClick.AddListener(() => windowsHandler.OpenWindow(WindowType.Settings));
            profileButton.button.onClick.AddListener(() => windowsHandler.OpenWindow(WindowType.Profile, true));

            var handler = new SwitchesHandler(new List<Switch>
                { shopButton, leaderboardButton, homeButton, dailyRewardButton, profileButton });

            usdPlusButton.onClick.AddListener(shopButton.button.onClick.Invoke);
            expProgressButton.onClick.AddListener(() => windowsHandler.OpenWindow(WindowType.LevelReward, true));

            expProgress.SetProgress(expHandler.LevelInfo.exp / (float)expHandler.ExpToLevelUp);
            
            ChangeCoins(currencyHandler.Currencies[CurrencyType.Coin].Counter.Value);
            currencyHandler.Currencies[CurrencyType.Coin].Counter.OnValueChanged += ChangeCoins;
            
            ChangeCash(currencyHandler.Currencies[CurrencyType.Cash].Counter.Value);
            currencyHandler.Currencies[CurrencyType.Cash].Counter.OnValueChanged += ChangeCash;
        }

        private void Start()
        {
            homeButton.button.onClick?.Invoke();
        }

        private void ChangeCash(int value)
        {
            cash.text = value.ToString() + '$';
        }

        private void ChangeCoins(int value)
        {
            coins.text = value.ToString();
        }
    }
}