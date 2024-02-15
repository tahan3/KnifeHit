using System;
using DG.Tweening;
using Source.Scripts.Knifes;
using Source.Scripts.Pool;
using UnityEngine;

namespace Source.Scripts.Spawn
{
    public class KnifeSpawner : ISpawner<Knife>
    {
        public event Action<Knife> OnSpawned;
        
        private IPool<Knife> _pool;

        private readonly Vector3 _initialSpawnPosition;
        private readonly Vector3 _startSpawnPosition;
        private readonly float _initialDuration;

        public KnifeSpawner(IPool<Knife> pool, Vector3 initialSpawnPosition, Vector3 startSpawnPosition, float initialDuration = 0.05f)
        {
            _pool = pool;
            _initialSpawnPosition = initialSpawnPosition;
            _startSpawnPosition = startSpawnPosition;
            _initialDuration = initialDuration;
        }

        public Knife Spawn()
        {
            var item = _pool.GetItem();

            item.SetFree();
            
            var transform = item.transform;
            
            transform.position = _startSpawnPosition;
            transform.rotation = Quaternion.identity;
            
            item.gameObject.SetActive(true);
            item.transform.DOMove(_initialSpawnPosition, _initialDuration).SetEase(Ease.Linear).onComplete += () => OnSpawned?.Invoke(item);
            
            return item;
        }
    }
}