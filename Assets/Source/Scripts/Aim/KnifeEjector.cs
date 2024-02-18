using DG.Tweening;
using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Events;
using Source.Scripts.Knifes;
using Source.Scripts.Particles;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Aim
{
    public class KnifeEjector : KnifeAim
    {
        [SerializeField] private float knifeForce = 3f;
        [SerializeField] private float knifeForceTime = 0.5f;

        [Inject] private MainEventsHandler _mainEvents;
        [Inject] private ParticlesHandler _particlesHandler;
        
        public override void GetKnife(Knife knife)
        {
            if (knife.state == KnifeState.Idle) return;

            _particlesHandler.PlayParticle(ParticleType.KnifeEject, knife.transform.position);
            
            knife.collider.enabled = false;
            knife.rigidbody.velocity = Vector3.zero;
            knife.transform.DOMove(knife.transform.forward * -knifeForce + Vector3.up * knifeForce, knifeForceTime);
            knife.transform.DORotate(new Vector3(0f, Random.Range(0f, 360f), 0f), knifeForceTime).onComplete +=
                () => knife.gameObject.SetActive(false);

            _mainEvents.OnKnifeEjected?.Invoke();
        }
    }
}