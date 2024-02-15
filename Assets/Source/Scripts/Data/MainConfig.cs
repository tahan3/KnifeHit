using System.Collections.Generic;
using Source.Scripts.Data.LevelData;
using UnityEngine;

namespace Source.Scripts.Data
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "MainConfig", order = 0)]
    public class MainConfig : ScriptableObject
    {
        public List<MissionConfig> missions;
    }
}