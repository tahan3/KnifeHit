using System.Collections.Generic;
using Source.Scripts.Aim;
using Source.Scripts.Aim.Bonuses;
using Source.Scripts.Knifes;
using Source.Scripts.Sequence;
using UnityEngine;
using UnityEngine.Serialization;

namespace Source.Scripts.Data.LevelData
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public KnifeAim mainKnifeAimPrefab;
        public Knife knifePrefab;
        public Bonus bonusPrefab;
        public int knifesToWin;

        public List<RotationSequence> mainAimRotationSequences;
        public List<Vector3> ejectorPositions;
        public List<Vector3> bonusPositions;
    }
}