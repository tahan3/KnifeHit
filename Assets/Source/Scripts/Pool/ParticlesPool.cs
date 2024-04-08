using UnityEngine;
using Zenject;

namespace Source.Scripts.Pool
{
    public class ParticlesPool : ComponentsPool<ParticleSystem>
    {
        public ParticlesPool(DiContainer container, ParticleSystem prefab, int initialPoolSize, Transform parent = null) : base(container, prefab, initialPoolSize, parent)
        {
        }

        protected override bool IsFreeItem(ParticleSystem item)
        {
            return !item.IsAlive();
        }

        protected override ParticleSystem OnItemCreated(ParticleSystem item)
        {
            item.Clear();
            return item;
        }
    }
}