using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Cysharp.Threading.Tasks;
using Source.Scripts.Currency;
using Source.Scripts.DailyReward;
using Source.Scripts.Data.Screen;
using Source.Scripts.Level;
using Source.Scripts.Prefs;
using Source.Scripts.Sounds;
using Source.Scripts.Tutorial;
using Source.Scripts.UI.ProgressBar;
using Source.Scripts.View.Animations;
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
        public TextMeshProUGUI progressText;

        [Header("Currency")] 
        public TextMeshProUGUI coins;
        public TextMeshProUGUI cash;

        [Header("Icons")] 
        public SerializedDictionary<DailyRewardType, Image> icons;
        
        [Header("Tutor")]
        public LastTutorWindow tutorWindow;
        
        private WindowsHandler _windowsHandler;
        private ExpHandler _expHandler;
        private CurrencyHandler _currencyHandler;
        private SoundsHandler _soundsHandler;
        private RewardAnimations _rewardAnimations;

        [Inject]
        public void Construct(WindowsHandler windowsHandler, ExpHandler expHandler, CurrencyHandler currencyHandler,
            SoundsHandler soundsHandler, RewardAnimations rewardAnimations)
        {
            _windowsHandler = windowsHandler;
            _expHandler = expHandler;
            _currencyHandler = currencyHandler;
            _soundsHandler = soundsHandler;
            _rewardAnimations = rewardAnimations;
            
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
            progressText.text = expHandler.LevelInfo.Value.exp.ToString() + '%';
            
            cash.text = CurrencyConverter.Convert(CurrencyType.Cash, currencyHandler.Currencies[CurrencyType.Cash].Counter.Value);
            coins.text = CurrencyConverter.Convert(CurrencyType.Coin, currencyHandler.Currencies[CurrencyType.Coin].Counter.Value);
            
            currencyHandler.Currencies[CurrencyType.Coin].Counter.OnValueChanged += ChangeCoins;
            currencyHandler.Currencies[CurrencyType.Cash].Counter.OnValueChanged += ChangeCash;
        }

        private void Start()
        {
            homeButton.button.onClick?.Invoke();

            LevelEarnedWindowOpen();
            
            if (PlayerPrefs.HasKey(PrefsNames.TutorStage.ToString()))
            {
                int lastTutorStage = 24;
                
                if (PlayerPrefs.GetInt(PrefsNames.TutorStage.ToString()) == lastTutorStage)
                {
                    tutorWindow.gameObject.SetActive(true);
                    tutorWindow.submitButton.onClick.AddListener(TutorClose);
                    PlayerPrefs.SetInt(PrefsNames.TutorStage.ToString(), ++lastTutorStage);
                }
                else
                {
                    DailyRewardWindowOpen();
                }
            }
            
            foreach (var keyValuePair in icons)
            {
                _rewardAnimations.SetCachedPosition(keyValuePair.Key, keyValuePair.Value.rectTransform);
            }
        }

        private void TutorClose()
        {
            tutorWindow.gameObject.SetActive(false);
            DailyRewardWindowOpen();
            _expHandler.GetExp(25);
            _currencyHandler.Currencies[CurrencyType.Coin].Counter.Value += 200;
        }

        private async void LevelEarnedWindowOpen()
        {
            await UniTask.WaitForSeconds(1f);
            
            if (_expHandler.LevelEarned)
            {
                _soundsHandler.PlaySound(SoundType.NewLevelEarned);
                _windowsHandler.OpenWindow(WindowType.LevelReward, true);
                _expHandler.LevelEarned = false;
            }
        }
        
        private void DailyRewardWindowOpen()
        {
            DateTime lastTime = DateTime.FromBinary(Convert.ToInt64(PlayerPrefs.GetString(
                RewardPrefs.Time.ToString(),
                DateTime.MinValue.ToBinary().ToString())));

            if (lastTime <= DateTime.Now.AddDays(-1))
            {
                _windowsHandler.OpenWindow(WindowType.DailyReward, true);
            }
        }
        
        private void ChangeCash(int value)
        {
            _soundsHandler.PlaySound(SoundType.Currency);
            cash.text = CurrencyConverter.Convert(CurrencyType.Cash, value);
        }

        private void ChangeCoins(int value)
        {
            _soundsHandler.PlaySound(SoundType.Currency);
            coins.text = CurrencyConverter.Convert(CurrencyType.Coin, value);
        }

        private void OnDisable()
        {
            _currencyHandler.Currencies[CurrencyType.Coin].Counter.OnValueChanged -= ChangeCoins;
            _currencyHandler.Currencies[CurrencyType.Cash].Counter.OnValueChanged -= ChangeCash;
        }
    }
}