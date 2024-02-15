using System.Collections.Generic;
using Source.Scripts.Data;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject;

namespace Source.Scripts.UI
{
    public class TestMissionsSpawner : MonoBehaviour
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
                item.playButton.onClick.AddListener(() => LoadMission(i1));
            }
        }

        private void LoadMission(int missionIndex)
        {
            _missionsHandler.currentMission = mainConfig.missions[missionIndex];

            SceneManager.LoadScene("MainGameplay");
        }
    }
}