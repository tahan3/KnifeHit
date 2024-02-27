using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Tutorial
{
    public class CursorAnimation : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private float _duration;
        [SerializeField] private int _loops = -1;
        [SerializeField] private LoopType _loopType;
        [SerializeField] private Ease _ease;

        private void Start()
        {
            transform.DOLocalMove(_endPoint.localPosition, _duration).SetLoops(_loops, _loopType).SetEase(_ease).SetUpdate(UpdateType.Normal, true);
        }
    }
}
