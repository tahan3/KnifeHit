using System;
using Source.Scripts.Currency;
using Source.Scripts.DailyReward;
using Source.Scripts.Level;
using Zenject;

namespace Source.Scripts.View.DailyReward
{
    public class DailyRewardWindowHandler
    {
        private DailyRewardWindow _dailyRewardWindow;

        [Inject] private WindowsHandler _windowsHandler;
        [Inject] private CurrencyHandler _currencyHandler;
        [Inject] private ExpHandler _expHandler;
        [Inject] private DailyRewardWindowData _dailyRewardWindowData;

        private DailyRewardLoader _dailyRewardLoader;
        
        public DailyRewardWindowHandler(DailyRewardWindow dailyRewardWindow)
        {
            _dailyRewardWindow = dailyRewardWindow;
            _dailyRewardLoader = new DailyRewardLoader();
        }

        public void Init()
        {
            _dailyRewardWindow.closeButton.gameObject.SetActive(false);
            _dailyRewardWindow.closeButton.onClick.AddListener(CloseButtonClick);

            InitItems();
            
            if (_dailyRewardLoader.Day < _dailyRewardWindow._items.Count)
            {
                _dailyRewardWindow._items[_dailyRewardLoader.Day].mainItem.image.sprite =
                    _dailyRewardWindowData.todaySprites.GetSprite(true);
                _dailyRewardWindow._items[_dailyRewardLoader.Day].mainItem.onClick.AddListener(GetReward);
                _dailyRewardWindow._items[_dailyRewardLoader.Day].dayNumberText.gameObject.SetActive(false);
            }
            else
            {
                _dailyRewardWindow._lastItem.mainItem.onClick.AddListener(GetReward);
            }
        }

        private void CloseButtonClick()
        {
            _windowsHandler.OpenPreviousWindow(true);
        }

        private void InitItems()
        {
            for (int i = 0; i < _dailyRewardWindow._items.Count; i++)
            {
                if (i < _dailyRewardLoader.Day)
                {
                    _dailyRewardWindow._items[i].SetActive(false);
                }
                else
                {
                    _dailyRewardWindow._items[i].rewardNumber.text = GetRewardText(
                        _dailyRewardWindowData.dailyRewardConfig.dailyRewards[i].rewardType,
                        _dailyRewardWindowData.dailyRewardConfig.dailyRewards[i].amount);
                    
                    if (_dailyRewardWindowData.rewardSprites.TryGetValue(
                            _dailyRewardWindowData.dailyRewardConfig.dailyRewards[i].rewardType, out var sprite))
                    {
                        _dailyRewardWindow._items[i].rewardIcon.sprite = sprite;
                    }
                }
            }

            for (var i = 0; i < _dailyRewardWindow._lastItem.icons.Count; i++)
            {
                if (_dailyRewardWindowData.rewardSprites.TryGetValue(
                        _dailyRewardWindowData.dailyRewardConfig.lastDayRewards[i].rewardType, out var sprite))
                {
                    _dailyRewardWindow._lastItem.icons[i].sprite = sprite;
                }

                _dailyRewardWindow._lastItem.rewardNumbers[i].text = GetRewardText(
                    _dailyRewardWindowData.dailyRewardConfig.lastDayRewards[i].rewardType,
                    _dailyRewardWindowData.dailyRewardConfig.lastDayRewards[i].amount);
            }
        }

        private string GetRewardText(DailyRewardType type, int amount)
        {
            switch (type)
            {
                case DailyRewardType.Cash:
                    return CurrencyConverter.Convert(CurrencyType.Cash, amount);
                case DailyRewardType.Coin:
                    return CurrencyConverter.Convert(CurrencyType.Coin, amount);
                default:
                    return amount.ToString();
            }
        }
        
        private void GetReward()
        {
            if (_dailyRewardLoader.Day < _dailyRewardWindow._items.Count)
            {
                _dailyRewardWindow._items[_dailyRewardLoader.Day].SetActive(false);
                _dailyRewardWindow._items[_dailyRewardLoader.Day].mainItem.image.sprite =
                    _dailyRewardWindowData.todaySprites.GetSprite(false);
                
                GetReward(_dailyRewardWindowData.dailyRewardConfig.dailyRewards[_dailyRewardLoader.Day]);
                _dailyRewardWindow._items[_dailyRewardLoader.Day].mainItem.onClick.RemoveAllListeners();
            }
            else
            {
                for (var i = 0; i < _dailyRewardWindowData.dailyRewardConfig.lastDayRewards.Count; i++)
                {
                    GetReward(_dailyRewardWindowData.dailyRewardConfig.lastDayRewards[i]);
                }

                _dailyRewardWindow._lastItem.mainItem.onClick.RemoveAllListeners();
            }
            
            _dailyRewardWindow.closeButton.gameObject.SetActive(true);
            _dailyRewardWindow.description.SetActive(true);
            
            _dailyRewardLoader.Save();
        }

        private void GetReward(DailyRewardData data)
        {
            switch (data.rewardType)
            {
                case DailyRewardType.Coin:
                    _currencyHandler.Currencies[CurrencyType.Coin].Counter.Value += data.amount;
                    _currencyHandler.Save();
                    break;
                case DailyRewardType.Cash:
                    _currencyHandler.Currencies[CurrencyType.Cash].Counter.Value += data.amount;
                    _currencyHandler.Save();
                    break;
                case DailyRewardType.Exp:
                    _expHandler.GetExp(data.amount);
                    _expHandler.Save();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(data.rewardType), data.rewardType, null);
            }
        }
    }
}