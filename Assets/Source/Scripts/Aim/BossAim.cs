using Source.Scripts.Data.ParticlesData;
using Source.Scripts.Data.Screen;
using Source.Scripts.Gameplay;
using Source.Scripts.Particles;
using Source.Scripts.View;
using Zenject;

namespace Source.Scripts.Aim
{
    public class BossAim : MainAim
    {
        [Inject] private ParticlesHandler _particlesHandler;
        [Inject] private WindowsHandler _windowsHandler;
        [Inject] private GameOverHandler _gameOverHandler;

        private void Start()
        {
            _gameOverHandler.OnGameOver += () => _windowsHandler.OpenWindow(WindowType.BossDefeated);
        }
        
        public override void Explosion()
        {
            _particlesHandler.PlayParticle(ParticleType.BossAimExplosion, knifesParent.position);
            
            base.Explosion();
        }
    }
}