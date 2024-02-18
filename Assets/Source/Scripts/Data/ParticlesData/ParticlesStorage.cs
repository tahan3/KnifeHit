using UnityEngine;

namespace Source.Scripts.Data.ParticlesData
{
    [CreateAssetMenu(fileName = "ParticlesStorage", menuName = "ParticlesStorage", order = 0)]
    public class ParticlesStorage : KeyValueStorage<ParticleType, ParticleSystem>
    {
    }
}