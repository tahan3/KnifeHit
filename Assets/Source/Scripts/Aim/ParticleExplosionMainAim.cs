using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Particles;
using Zenject;

namespace Source.Scripts.Aim
{
    public class ParticleExplosionMainAim : MainAim
    {
        [Inject] private ParticlesHandler _particlesHandler;

        public override void Explosion()
        {
            _particlesHandler.PlayParticle(ParticleType.MainAimExplosion, knifesParent.position);
            
            base.Explosion();
        }
    }
}