using System.Collections.Generic;
using Source.Scripts.Data;
using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Pool;
using UnityEngine;

namespace Source.Scripts.Particles
{
    public class ParticlesHandler
    {
        private KeyValueStorage<ParticleType, ParticleSystem> _particlesStorage;

        private Dictionary<ParticleType, IPool<ParticleSystem>> particlesPools;

        public ParticlesHandler(KeyValueStorage<ParticleType, ParticleSystem> particlesStorage, IPool<ParticleSystem> particlesPool)
        {
            _particlesStorage = particlesStorage;
        }

        public void PlayParticle(ParticleType type, Vector3 position)
        {
            var item = particlesPools[type].GetItem();
            item.transform.position = position;
            item.Play();
            //check item.IsAlive()
        }
    }
}