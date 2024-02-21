using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Events;
using Source.Scripts.Gameplay;
using Source.Scripts.Knifes;
using Source.Scripts.Particles;
using Source.Scripts.Sequence;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Source.Scripts.Aim
{
    public class MainAim : KnifeAim
    {
        [SerializeField] protected Transform knifesParent;
        [SerializeField] private KnifeEjector ejectorPrefab;
        [SerializeField] private float forceValue;
        [SerializeField] private float forceTime;

        private MainEventsHandler _mainEvents;
        private ParticlesHandler _particlesHandler;
        private DiContainer _container;

        [Inject]
        public void Construct(MainEventsHandler mainEvents, GameOverHandler gameOverHandler, LevelConfig levelConfig, DiContainer container, ParticlesHandler particlesHandler)
        {
            _container = container;
            _mainEvents = mainEvents;
            _particlesHandler = particlesHandler;
            
            gameOverHandler.OnLevelEnded += Explosion;

            InstantiateEjectors(levelConfig);
            InstantiateBonuses(levelConfig);
            StartRotation(levelConfig);
        }
        
        public override void GetKnife(Knife knife)
        {
            knife.transform.SetParent(knifesParent);
            knife.SetIdle();

            _particlesHandler.PlayParticle(ParticleType.KnifeHitAim, knife.transform.position);
            
            Debug.Log("Get knife");
            
            _mainEvents.OnKnifeHitAim?.Invoke();
        }

        public virtual void Explosion()
        {
            for (int i = 0; i < knifesParent.childCount; i++)
            {
                var child = knifesParent.GetChild(i);

                Vector3 forceDirection = (child.position - knifesParent.position).normalized;
                child.DOMove(-forceDirection * forceValue, forceTime);
                child.DORotate(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)),
                    forceTime, RotateMode.FastBeyond360);
            }

            knifesParent.DetachChildren();
        }
        
        private async UniTask StartRotation(LevelConfig levelConfig)
        {
            var cancellationToken = gameObject.GetCancellationTokenOnDestroy();
            
            if (levelConfig.mainAimRotationSequences.Count == 0) return;
            
            int index = 0;

            while (enabled)
            {
                await knifesParent
                    .DORotate(Vector3.up * levelConfig.mainAimRotationSequences[index].angle,
                        levelConfig.mainAimRotationSequences[index].time,
                        RotateMode.WorldAxisAdd).SetEase(levelConfig.mainAimRotationSequences[index].ease)
                    .AsyncWaitForCompletion().AsUniTask().AttachExternalCancellation(cancellationToken);

                index = index >= levelConfig.mainAimRotationSequences.Count - 1 ? 0 : index + 1;
            }
        }

        private void InstantiateEjectors(LevelConfig levelConfig)
        {
            for (int i = 0; i < levelConfig.ejectorPositions.Count; i++)
            {
                var ejector = _container.InstantiatePrefab(ejectorPrefab, knifesParent);
                ejector.transform.localPosition = levelConfig.ejectorPositions[i];
                ejector.transform.LookAt(knifesParent, Vector3.up);
            }
        }

        private void InstantiateBonuses(LevelConfig levelConfig)
        {
            for (int i = 0; i < levelConfig.bonusPositions.Count; i++)
            {
                var bonus = _container.InstantiatePrefab(levelConfig.bonusPrefab, knifesParent);
                bonus.transform.localPosition = levelConfig.bonusPositions[i];
                bonus.transform.LookAt(knifesParent, Vector3.up);
            }
        }

        private void OnDisable()
        {
            transform.DOKill();
        }
    }
}