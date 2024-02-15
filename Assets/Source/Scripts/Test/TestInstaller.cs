using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Aim;
using Source.Scripts.Counter;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Events;
using Source.Scripts.Gameplay;
using Source.Scripts.Knifes;
using Source.Scripts.Pool;
using Source.Scripts.Spawn;
using Source.Scripts.Thrower;
using Source.Scripts.UI.ProgressBar;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Test
{
    public class TestInstaller : MonoInstaller
    {
        //[SerializeField] private List<StageConfig> stages;
        [SerializeField] private Button throwKnifeButton;
        [SerializeField] private Transform knifesParent;
        
        [Header("Settings")]
        [SerializeField] private Vector3 initialSpawnPosition;
        [SerializeField] private Vector3 initialStartPosition;
        [SerializeField] private float knifeForce;

        [Header("View")] 
        [SerializeField] private StepProgress knifesProgress;
        [SerializeField] private StepProgress levelProgress;
        
        private MainEventsHandler _mainEvents;
        private GameOverHandler _gameOverHandler;
        
        private LevelHandler _levelHandler;
        private StageHandler _stageHandler;
        
        private IPool<Knife> _pool;
        private ISpawner<Knife> _spawner;
        private IThrower<Knife> _thrower;

        private KnifesHandler _knifesHandler;

        private ICounter _knifesPerRoundCounter;
        
        [Inject]
        private MissionsHandler _missionsHandler;
        
        public override void InstallBindings()
        {
            _mainEvents = new MainEventsHandler();
            _levelHandler = new LevelHandler();
            _stageHandler = new StageHandler();

            if (_stageHandler.Stage >= _missionsHandler.currentMission.stages.Count)
            {
                _stageHandler.Stage = 0;
                _levelHandler.Clear();
                _stageHandler.Clear();
                SceneManager.LoadScene("MainMenu");
            }
            
            if (_levelHandler.Level >= _missionsHandler.currentMission.stages[_stageHandler.Stage].levels.Count)
            {
                _levelHandler.Level = 0;
                _stageHandler.IncrementStage();
            }
            
            if (_stageHandler.Stage >= _missionsHandler.currentMission.stages.Count)
            {
                _stageHandler.Stage = 0;
                _levelHandler.Clear();
                _stageHandler.Clear();
                SceneManager.LoadScene("MainMenu");
            }

            Container.Bind<MainEventsHandler>().FromInstance(_mainEvents).AsSingle();
            Container.Bind<LevelConfig>().FromInstance(_missionsHandler.currentMission.stages[_stageHandler.Stage].levels[_levelHandler.Level]).AsSingle();
            Container.Bind<StageConfig>().FromInstance(_missionsHandler.currentMission.stages[_stageHandler.Stage]).AsSingle();
            Container.Bind<LevelHandler>().FromInstance(_levelHandler).AsSingle();
            
            _knifesPerRoundCounter = new KnifesPerRoundCounter();

            _knifesPerRoundCounter.CounterNumber.OnValueChanged += knifesProgress.SetProgress;
            
            ICounter totalBonusesCounter = new TotalBonusesCounter();
            ICounter totalKnifesCounter = new TotalKnifesCounter();

            _gameOverHandler =
                new GameOverHandler(_knifesPerRoundCounter, _missionsHandler.currentMission.stages[_stageHandler.Stage].levels[_levelHandler.Level].knifesToWin);

            Container.Bind<GameOverHandler>().FromInstance(_gameOverHandler).AsSingle();

            _gameOverHandler.OnGameOver += () => throwKnifeButton.enabled = false;
            _gameOverHandler.OnGameOver += _levelHandler.IncrementLevel;
            _gameOverHandler.OnGameOver += () => knifesParent.gameObject.SetActive(false);

            _mainEvents.OnKnifeHitAim += () => _knifesPerRoundCounter.CounterNumber.Value++;
            _mainEvents.OnKnifeHitAim += () => totalKnifesCounter.CounterNumber.Value++;
            
            _mainEvents.OnKnifeHitBonus += () => totalBonusesCounter.CounterNumber.Value++;

            Container.InstantiatePrefab(_missionsHandler.currentMission.stages[_stageHandler.Stage].levels[_levelHandler.Level].mainKnifeAimPrefab);
            
            _pool = new ComponentsPool<Knife>(Container, _missionsHandler.currentMission.stages[_stageHandler.Stage].levels[_levelHandler.Level].knifePrefab,
                _missionsHandler.currentMission.stages[_stageHandler.Stage].levels[_levelHandler.Level].knifesToWin, knifesParent);
            _spawner = new KnifeSpawner(_pool, initialSpawnPosition, initialStartPosition);
            _thrower = new KnifesThrower(knifeForce);
            
            _knifesHandler = new KnifesHandler(_spawner, _thrower, throwKnifeButton);

            _mainEvents.OnKnifeHitAim += () => _spawner.Spawn();
            _mainEvents.OnKnifeEjected += () => _spawner.Spawn();

            _spawner.Spawn();
        }
    }
}