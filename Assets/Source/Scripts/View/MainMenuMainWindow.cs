using System.Collections.Generic;
using Source.Scripts.Data.Screen;
using Source.Scripts.View.Buttons;
using Source.Scripts.View.Windows;
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
        
        [Inject]
        public void Construct(WindowsHandler windowsHandler)
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
        }

        private void Start()
        {
            homeButton.button.onClick?.Invoke();
        }
    }
}