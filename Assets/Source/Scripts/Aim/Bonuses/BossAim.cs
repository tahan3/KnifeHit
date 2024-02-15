using UnityEngine;

namespace Source.Scripts.Aim.Bonuses
{
    public class BossAim : DefaultKnifeAim
    {
        [SerializeField] private ParticleSystem explosionParticle;
        
        public override void Explosion()
        {
            var particle = Instantiate(explosionParticle);
            particle.Play();
            
            base.Explosion();
        }
    }
}