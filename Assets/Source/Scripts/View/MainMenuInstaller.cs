using Source.Scripts.Data.Screen;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View
{
    public class MainMenuInstaller : MonoBehaviour
    {
        public Toggle leaderboardButton;
        public Toggle shopButton;
        public Toggle homeButton;
        public Toggle dailyRewardButton;
        public Toggle profileButton;
        public Button settingsButton;
        
        [Inject] private WindowsHandler _windowsHandler;

        private void Start()
        {
            //leaderboardButton.onClick.AddListener(()=>_windowsHandler.OpenWindow());
            shopButton.onValueChanged.AddListener((bool x)=>_windowsHandler.OpenWindow(WindowType.Shop, true));
            homeButton.onValueChanged.AddListener((bool x)=>_windowsHandler.OpenWindow(WindowType.Missions, true));
            dailyRewardButton.onValueChanged.AddListener((bool x)=>_windowsHandler.OpenWindow(WindowType.DailyReward, true));
            settingsButton.onClick.AddListener(() => _windowsHandler.OpenWindow(WindowType.Settings));
            profileButton.onValueChanged.AddListener((bool x) => _windowsHandler.OpenWindow(WindowType.Profile, true));
        }
    }
}