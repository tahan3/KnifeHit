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

        [SerializeField] private MainConfig mainConfig;
        [SerializeField] private KeyValueStorage<CurrencyType, Sprite> currencySprites;

        private MissionsHandler _missionsHandler;
        
        [Inject]
        private void Construct(MissionsHandler missionsHandler)
        {
            _missionsHandler = missionsHandler;
            
            for (var i = 0; i < mainConfig.missions.Count; i++)
            {
                var item = Instantiate(prefab, parent);
                var i1 = i;

                item.missionNameText.text = mainConfig.missions[i].missionName;
                item.playerCountText.text = mainConfig.missions[i].playersLimit + " players";

                int minutes = mainConfig.missions[i].time / 60;
                int seconds = mainConfig.missions[i].time % 60;

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
                }
                else
                {
                    item.Close();
                }

                if (currencySprites.TryGetValue(mainConfig.missions[i].reward.currency, out var sprite))
                {
                    item.rewardIcon.sprite = sprite;
                    item.rewardText.text = CurrencyConverter.Convert(mainConfig.missions[i].reward.currency, mainConfig.missions[i].reward.amount);
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
            _missionsHandler.LoadMission(mainConfig.missions[missionIndex]);
        }
    }
}