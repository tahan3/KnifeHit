using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Data.LevelData
{
    [CreateAssetMenu(fileName = "StageConfig", menuName = "StageConfig", order = 0)]
    public class StageConfig : ScriptableObject
    {
        public List<LevelConfig> levels;
    }
}