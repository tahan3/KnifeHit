using Source.Scripts.Data.ParticlesData;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Particles
{
    public class ParticlesInstaller : MonoInstaller
    {
        [SerializeField] private ParticlesStorage particlesStorage;
        
        public override void InstallBindings()
        {
            ParticlesHandler particlesHandler = new ParticlesHandler(particlesStorage, Container, transform);

            Container.Bind<ParticlesHandler>().FromInstance(particlesHandler).AsSingle();
        }
    }
}