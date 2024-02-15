using System;
using DG.Tweening;

namespace Source.Scripts.Sequence
{
    [Serializable]
    public struct RotationSequence
    {
        public float angle;
        public float time;
        public Ease ease;
    }
}