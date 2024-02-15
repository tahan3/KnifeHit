using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Data.LevelData
{
    [CreateAssetMenu(fileName = "MissionConfig", menuName = "MissionConfig", order = 0)]
    public class MissionConfig : ScriptableObject
    {
        public List<StageConfig> stages;
    }
}