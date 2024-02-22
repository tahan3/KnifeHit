using DG.Tweening;
using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Events;
using Source.Scripts.Knifes;
using Source.Scripts.Particles;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Aim.Bonuses
{
    public class Bonus : KnifeAim
    {
        [SerializeField] private float forceValue;
        [SerializeField] private float forceTime;
        [SerializeField] private Transform pointsParticle;
        
        [Inject] private MainEventsHandler _mainEvents;
        [Inject] private ParticlesHandler _particlesHandler;
        
        public override void GetKnife(Knife knife)
        {
            _particlesHandler.PlayParticle(ParticleType.BonusAimExplosion, transform.position);
            
            _mainEvents.OnKnifeHitBonus?.Invoke();

            ThrowSelfParticle();
            BonusAnimation();
        }

        private void ThrowSelfParticle()
        {
            pointsParticle.gameObject.SetActive(true);
            pointsParticle.SetParent(null);
            pointsParticle.DOMove(pointsParticle.position + Vector3.up * 2, 0.25f).onComplete +=
                () => pointsParticle.gameObject.SetActive(false);
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
            gameObject.SetActive(false);
        }
    }
}