using System;
using Source.Scripts.Currency;
using Source.Scripts.Data;
using Source.Scripts.Gameplay;
using Source.Scripts.UI;
using Source.Scripts.View.Windows;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Source.Scripts.View.Missions
{
    public class MissionsWindow : Window
    {
        [SerializeField] private MissionPrefab prefab;
        [SerializeField] private Transform parent;

        [SerializeField] private KeyValueStorage<CurrencyType, Sprite> currencySprites;

        private MissionsHandler _missionsHandler;
        private MainConfig _mainConfig;
        
        [Inject]
        private void Construct(MissionsHandler missionsHandler, CurrencyHandler currencyHandler, MainConfig mainConfig)
        {
            _missionsHandler = missionsHandler;
            _mainConfig = mainConfig;
            
            for (var i = 0; i < _mainConfig.missions.Count; i++)
            {
                var item = Instantiate(prefab, parent);
                var i1 = i;

                item.missionNameText.text = _mainConfig.missions[i].missionName;
                item.playerCountText.text = _mainConfig.missions[i].playersLimit + " players";

                int minutes = _mainConfig.missions[i].time / 60;
                int seconds = _mainConfig.missions[i].time % 60;

                if (seconds > 0)
                {
                    item.timeLimitText.text = "Time Limit: " + minutes + " minutes " + seconds + " seconds";
                }
                else
                {
                    item.timeLimitText.text = "Time Limit: " + minutes + " minutes";
                }

                if (i <= _missionsHandler.OpenedMissions)
                {
                    item.Open();
                    item.playButton.onClick.AddListener(() => SetMission(i1));

                    if (_mainConfig.missions[i1].cost > 0)
                    {
                        item.SetCost(_mainConfig.missions[i1].cost);
                        item.playButton.onClick.AddListener(() =>
                        {
                            currencyHandler.Currencies[CurrencyType.Coin].Counter.Value -=
                                _mainConfig.missions[i1].cost;
                            currencyHandler.Save();
                        });

                        var status = _mainConfig.missions[i1].cost >=
                                     currencyHandler.Currencies[CurrencyType.Coin].Counter.Value;
                        
                        item.playButton.gameObject.SetActive(!status);
                        item.lockPlayButton.gameObject.SetActive(status);
                        
                        currencyHandler.Currencies[CurrencyType.Coin].Counter.OnValueChanged += (int cost) =>
                        {
                            item.playButton.gameObject.SetActive(!(_mainConfig.missions[i1].cost >= cost));
                            item.lockPlayButton.gameObject.SetActive(_mainConfig.missions[i1].cost >= cost);
                        };
                    }
                }
                else
                {
                    item.Close();
                }

                if (currencySprites.TryGetValue(_mainConfig.missions[i].reward.currency, out var sprite))
                {
                    item.rewardIcon.sprite = sprite;
                    item.rewardText.text = CurrencyConverter.Convert(_mainConfig.missions[i].reward.currency, _mainConfig.missions[i].reward.amount);
                }
            }
        }

        public override void Open()
        {
            gameObject.SetActive(true);
        }

        public override void Close()
        {
            gameObject.SetActive(false);
        }

        private void SetMission(int missionIndex)
        {
            _missionsHandler.LoadMission(_mainConfig.missions[missionIndex]);
        }
    }
}