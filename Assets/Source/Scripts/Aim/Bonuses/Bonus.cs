using DG.Tweening;
using Source.Scripts.Events;
using Source.Scripts.Knifes;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Aim.Bonuses
{
    public class Bonus : KnifeAim
    {
        [SerializeField] private float forceValue;
        [SerializeField] private float forceTime;
        
        [Inject] private MainEventsHandler _mainEvents;
        
        public override void GetKnife(Knife knife)
        {
            _mainEvents.OnKnifeHitBonus?.Invoke();

            BonusAnimation();
        }

        protected virtual void BonusAnimation()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                var child = transform.GetChild(i);

                Vector3 forceDirection = (child.position - transform.position).normalized;
                child.DOMove(-forceDirection * forceValue, forceTime);
                child.DORotate(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)),
                    forceTime, RotateMode.FastBeyond360);
            }

            transform.DetachChildren();
        }
    }
}