using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Currency;
using Source.Scripts.Data.LevelReward;
using Source.Scripts.Data.Screen;
using Source.Scripts.Level;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.LevelReward
{
    public class LevelRewardWindowHandler
    {
        private LevelRewardWindow _levelRewardWindow;

        private WindowsHandler _windowsHandler;
        private LevelRewardConfig _levelRewardConfig;
        private LevelRewardWindowData _levelRewardWindowData;
        private ExpHandler _expHandler;

        private LevelRewardHandler _levelRewardHandler;

        private List<LevelRewardProgressBar> _items;
        
        public LevelRewardWindowHandler(LevelRewardWindow levelRewardWindow)
        {
            _levelRewardWindow = levelRewardWindow;
            _items = new List<LevelRewardProgressBar>();
        }

        [Inject]
        public void Construct(WindowsHandler windowsHandler, LevelRewardConfig levelRewardConfig,
            LevelRewardWindowData levelRewardWindowData, ExpHandler expHandler, CurrencyHandler currencyHandler)
        {
            _windowsHandler = windowsHandler;
            _levelRewardConfig = levelRewardConfig;
            _levelRewardWindowData = levelRewardWindowData;
            _expHandler = expHandler;

            _levelRewardHandler = new LevelRewardHandler(_levelRewardConfig, _expHandler.LevelInfo.Value, currencyHandler);
        }

        public void Init()
        {
            _levelRewardWindow.closeButton.onClick.AddListener(
                () => _windowsHandler.OpenPreviousWindow(true));
            
            for (var i = 0; i < _levelRewardConfig.rewardPerLevel.Count; i++)
            {
                LevelRewardProgressBar item;

                if (i <= 0)
                {
                    item = CreateFirst();
                }else if (i >= _levelRewardConfig.rewardPerLevel.Count - 1)
                {
                    item = CreateLast();
                }
                else
                {
                    item = CreateMiddle();
                }

                if (_levelRewardWindowData.rewardSprites.TryGetValue(_levelRewardConfig.rewardPerLevel[i].currency,
                        out var sprite))
                {
                    item.rewardIcon.sprite = sprite;
                    item.rewardIcon.SetNativeSize();
                }

                item.rewardText.text = CurrencyConverter.Convert(_levelRewardConfig.rewardPerLevel[i].currency,
                    _levelRewardConfig.rewardPerLevel[i].amount);

                item.rewardTakenMark.SetActive(i <= _levelRewardHandler.CollectedRewards.level);

                item.canTakeMark.SetActive(i > _levelRewardHandler.CollectedRewards.level &&
                                           i <= _expHandler.LevelInfo.Value.level);

                if (i == _expHandler.LevelInfo.Value.level + 1)
                {
                    item.SetProgress(_expHandler.LevelInfo.Value.exp);
                }
                else if (i <= _expHandler.LevelInfo.Value.level)
                {
                    item.SetProgress(100);
                }
                else
                {
                    item.SetProgress(0);
                }

                item.levelNumberText.text = (i + 1).ToString();
                
                _items.Add(item);
            }

            _levelRewardWindow.claimButton.image.sprite =
                _levelRewardWindowData.claimButtonSprites.GetSprite(_levelRewardHandler.CollectedRewards.level <
                                                                    _expHandler.LevelInfo.Value.level);
            _levelRewardWindow.claimButton.onClick.AddListener(ClaimButtonClick);
        }

        public void Rescale()
        {
            float perItemHeight = _levelRewardWindow.itemsContainer.sizeDelta.y / (_items.Count - 1);

            _levelRewardWindow.itemsContainer.DOKill();

            _levelRewardWindow.itemsContainer.anchoredPosition = new Vector2(
                _levelRewardWindow.itemsContainer.anchoredPosition.x,
                _levelRewardWindow.itemsContainer.sizeDelta.y);

            _levelRewardWindow.itemsContainer.DOAnchorPos(new Vector2(
                _levelRewardWindow.itemsContainer.anchoredPosition.x,
                _levelRewardWindow.itemsContainer.sizeDelta.y -
                (perItemHeight * (_expHandler.LevelInfo.Value.level))), 0.5f).SetEase(Ease.InCubic);

            /*_levelRewardWindow.itemsContainer.anchoredPosition = new Vector2(
                _levelRewardWindow.itemsContainer.anchoredPosition.x,
                _levelRewardWindow.itemsContainer.sizeDelta.y -
                (perItemHeight * (_expHandler.LevelInfo.Value.level)));*/

            //_levelRewardWindow.itemsContainer.anchoredPosition = ;
        }
        
        private void ClaimButtonClick()
        {
            if (_levelRewardHandler.CollectedRewards.level < _expHandler.LevelInfo.Value.level)
            {
                int currentLevel = _levelRewardHandler.CollectedRewards.level;

                if (currentLevel < 0)
                {
                    currentLevel = 0;
                }
                
                _levelRewardHandler.ClaimRewards();

                for (int i = currentLevel; i <= _expHandler.LevelInfo.Value.level; i++)
                {
                    _items[i].canTakeMark.SetActive(false);
                    _items[i].rewardTakenMark.SetActive(true);
                }

                _levelRewardWindow.claimButton.image.sprite =
                    _levelRewardWindowData.claimButtonSprites.GetSprite(false);
            }
        }
        
        private LevelRewardProgressBar CreateFirst()
        {
            return Object.Instantiate(_levelRewardWindowData.firstItem, _levelRewardWindow.itemsContainer);
        }
        
        private LevelRewardProgressBar CreateMiddle()
        {
            return Object.Instantiate(_levelRewardWindowData.midItem, _levelRewardWindow.itemsContainer);
        }
        
        private LevelRewardProgressBar CreateLast()
        {
            return Object.Instantiate(_levelRewardWindowData.lastItem, _levelRewardWindow.itemsContainer);
        }
    }
}