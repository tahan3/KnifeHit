using System;
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
                item.timeLimitText.text = "Time Limit: " + minutes + " minutes";
                
                
                item.playButton.onClick.AddListener(() => SetMission(i1));
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
            _missionsHandler.SetMission(mainConfig.missions[missionIndex]);
        }
    }
}