using System;
using System.Collections.Generic;
using AYellowpaper.SerializedCollections;
using Source.Scripts.Currency;
using Source.Scripts.Data.LevelReward;
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
        public int exp = 25;

        [Header("BG")] 
        public Sprite bgSprite;

        [Header("Mission rewards")]
        public RewardData reward;

        [Header("Cost")] 
        public int cost;
    }
}