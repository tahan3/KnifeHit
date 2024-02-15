using UnityEngine;

namespace Source.Scripts.Data.ParticlesData
{
    public class ParticlesStorage : KeyValueStorage<ParticleType, ParticleSystem>
    {
        public override ParticleSystem GetValue(ParticleType key)
        {
            return items[key];
        }
    }
}