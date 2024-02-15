using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Events;
using Source.Scripts.Gameplay;
using Source.Scripts.Knifes;
using Source.Scripts.Sequence;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Aim
{
    public class DefaultKnifeAim : KnifeAim
    {
        [SerializeField] protected Transform knifesParent;
        [SerializeField] private KnifeEjector ejectorPrefab;
        [SerializeField] private float forceValue;

        private MainEventsHandler _mainEvents;
        private DiContainer _container;

        [Inject]
        public void Construct(MainEventsHandler mainEvents, GameOverHandler gameOverHandler, LevelConfig levelConfig, DiContainer container)
        {
            _container = container;
            _mainEvents = mainEvents;
            
            gameOverHandler.OnGameOver += Explosion;

            StartRotation(levelConfig);
        }
        
        public override void GetKnife(Knife knife)
        {
            knife.transform.SetParent(knifesParent);
            knife.SetIdle();
            
            _mainEvents.OnKnifeHitAim?.Invoke();
        }

        public virtual void Explosion()
        {
            for (int i = 0; i < knifesParent.childCount; i++)
            {
                var child = knifesParent.GetChild(i);

                Vector3 forceDirection = (child.position - knifesParent.position).normalized;
                
                child.DOMove(-forceDirection * forceValue, 0.5f);
                child.DORotate(new Vector3(Random.Range(0f, 360f), Random.Range(0f, 360f), Random.Range(0f, 360f)),
                    0.5f, RotateMode.FastBeyond360);
            }

            knifesParent.DetachChildren();
        }
        
        private async UniTask StartRotation(LevelConfig levelConfig)
        {
            for (int i = 0; i < levelConfig.ejectorPositions.Count; i++)
            {
                var ejector = _container.InstantiatePrefab(ejectorPrefab, knifesParent);
                ejector.transform.localPosition = levelConfig.ejectorPositions[i];
                ejector.transform.LookAt(knifesParent, Vector3.up);
            }
            
            if (levelConfig.mainAimRotationSequences.Count == 0) return;
            
            int index = 0;

            while (enabled)
            {
                await knifesParent
                    .DORotate(Vector3.up * levelConfig.mainAimRotationSequences[index].angle,
                        levelConfig.mainAimRotationSequences[index].time,
                        RotateMode.WorldAxisAdd).SetEase(levelConfig.mainAimRotationSequences[index].ease)
                    .AsyncWaitForCompletion();

                index = index >= levelConfig.mainAimRotationSequences.Count - 1 ? 0 : index + 1;
            }
        }
    }
}