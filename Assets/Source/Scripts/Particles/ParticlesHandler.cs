using System.Collections.Generic;
using Source.Scripts.Data;
using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Pool;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Particles
{
    public class ParticlesHandler
    {
        private KeyValueStorage<ParticleType, ParticleSystem> _particlesStorage;

        private Dictionary<ParticleType, IPool<ParticleSystem>> particlesPools;

        private const int ParticleCount = 5;
        
        public ParticlesHandler(KeyValueStorage<ParticleType, ParticleSystem> particlesStorage, DiContainer container, Transform parent = null)
        {
            _particlesStorage = particlesStorage;
            
            particlesPools = new Dictionary<ParticleType, IPool<ParticleSystem>>();
            
            foreach (var key in _particlesStorage.GetKeys())
            {
                if (_particlesStorage.TryGetValue(key, out var value))
                {
                    particlesPools.Add(key,
                        new ParticlesPool(container, value, ParticleCount, parent));
                }
            }
        }

        public void PlayParticle(ParticleType type, Vector3 position)
        {
            var item = particlesPools[type].GetItem();
            item.transform.position = position;
            item.Play();
        }
    }
}