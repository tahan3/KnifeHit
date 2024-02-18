using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Data.LevelData
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "MissionConfig", order = 0)]
    public class MissionConfig : ScriptableObject
    {
        [Header("Waves Info")]
        public List<StageConfig> stages;

        [Header("Settings")] 
        public string missionName;
        public int playersLimit;
        public int time;
    }
}