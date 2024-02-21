using Source.Scripts.Data.LevelData;
using Source.Scripts.Events;
using Source.Scripts.Knifes;
using Source.Scripts.Pool;
using Source.Scripts.Spawn;
using Source.Scripts.Thrower;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Gameplay
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private Button throwKnifeButton;
        [SerializeField] private Transform knifesParent;

        [Header("BG")] 
        [SerializeField] private Image bgImage;
        
        [Header("Settings")]
        [SerializeField] private Vector3 initialSpawnPosition;
        [SerializeField] private Vector3 initialStartPosition;
        [SerializeField] private float knifeForce;
        
        private IPool<Knife> _pool;
        private ISpawner<Knife> _spawner;
        private IThrower<Knife> _thrower;
        
        [Inject] private LevelConfig _levelConfig;
        [Inject] private GameOverHandler _gameOverHandler;
        [Inject] private MainEventsHandler _mainEventsHandler;
        [Inject] private MissionsHandler _missionsHandler;
        
        public override void InstallBindings()
        {
            _pool = new ComponentsPool<Knife>(Container, _levelConfig.knifePrefab, _levelConfig.knifesToWin,
                knifesParent);
            _spawner = new KnifeSpawner(_pool, initialSpawnPosition, initialStartPosition);
            _thrower = new KnifesThrower(knifeForce);
            
            _gameOverHandler.OnLevelEnded += () => throwKnifeButton.enabled = false;
            _gameOverHandler.OnLevelEnded += () => knifesParent.gameObject.SetActive(false);
            
            var knifesHandler = new KnifesHandler(_spawner, _thrower, throwKnifeButton);

            _mainEventsHandler.OnKnifeHitAim += () => _spawner.Spawn();
            _mainEventsHandler.OnKnifeEjected += () => _spawner.Spawn();

            bgImage.sprite = _missionsHandler.Mission.bgSprite;

            Container.InstantiatePrefab(_levelConfig.mainKnifeAimPrefab);
            
            _spawner.Spawn();
        }
    }
}