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

        private bool _canSpawnNew;

        public KnifeSpawner(IPool<Knife> pool, Vector3 initialSpawnPosition, Vector3 startSpawnPosition, float initialDuration = 0.1f)
        {
            _pool = pool;
            _initialSpawnPosition = initialSpawnPosition;
            _startSpawnPosition = startSpawnPosition;
            _initialDuration = initialDuration;

            _canSpawnNew = true;
        }

        public Knife Spawn()
        {
            if (_canSpawnNew)
            {
                _canSpawnNew = false;
                var item = _pool.GetItem();

                item.SetFree();

                var transform = item.transform;

                transform.position = _startSpawnPosition;
                transform.rotation = Quaternion.identity;

                item.SetActive(false);
                item.gameObject.SetActive(true);
                item.transform.DOMove(_initialSpawnPosition, _initialDuration).SetEase(Ease.Linear).onComplete +=
                    () =>
                    {
                        _canSpawnNew = true;
                        OnSpawned?.Invoke(item);
                    };

                return item;
            }

            return null;
        }
    }
}