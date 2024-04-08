using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.Aim;
using Source.Scripts.Counter;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Events;
using Source.Scripts.Gameplay;
using Source.Scripts.Knifes;
using Source.Scripts.Leaderboard;
using Source.Scripts.Pool;
using Source.Scripts.Spawn;
using Source.Scripts.Thrower;
using Source.Scripts.UI.ProgressBar;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.Test
{
    public class GamePlayBinder : MonoInstaller
    {
        [SerializeField] private int pointsPerHitAim = 100;
        [SerializeField] private int pointsPerHitBonus = 200;
        [SerializeField] private float multiplierPerHitBonus = 0.1f;
        
        private MainEventsHandler _mainEvents;
        private GameOverHandler _gameOverHandler;

        private KnifesPerRoundCounter _knifesPerRoundCounter;
        
        [Inject] private MissionsHandler _missionsHandler;
        
        public override void InstallBindings()
        {
            Container.Bind<ILeaderboardHandler>().To<PlayFabLeaderboardHandler>().FromNew().AsSingle().NonLazy();
            
            _mainEvents = new MainEventsHandler();
            _knifesPerRoundCounter = new KnifesPerRoundCounter();

            _gameOverHandler = new GameOverHandler(_knifesPerRoundCounter, _missionsHandler);
            Container.Inject(_gameOverHandler);
            
            Container.Bind<MainEventsHandler>().FromInstance(_mainEvents).AsSingle();
            Container.Bind<LevelConfig>().FromInstance(_missionsHandler.Mission.stages[_missionsHandler.Stage].levels[_missionsHandler.Level]).AsSingle();
            Container.Bind<StageConfig>().FromInstance(_missionsHandler.Mission.stages[_missionsHandler.Stage]).AsSingle();
            Container.Bind<KnifesPerRoundCounter>().FromInstance(_knifesPerRoundCounter).AsSingle();
            Container.Bind<GameOverHandler>().FromInstance(_gameOverHandler).AsSingle();

            _mainEvents.OnKnifeHitAim += () =>
                _missionsHandler.PointsCounter.Counter.Value += (int)(pointsPerHitAim * _missionsHandler.Multiplier.Multiplier.Value);
            _mainEvents.OnKnifeHitAim += _missionsHandler.Multiplier.IncreaseMultiplier;
            _mainEvents.OnKnifeHitAim += () => _knifesPerRoundCounter.Counter.Value++;

            _mainEvents.OnKnifeEjected += _missionsHandler.Multiplier.SetDefaultMultiplier;

            _mainEvents.OnKnifeHitBonus += () => _missionsHandler.PointsCounter.Counter.Value += pointsPerHitBonus;
            _mainEvents.OnKnifeHitBonus += () => _missionsHandler.Multiplier.IncreaseMultiplier(multiplierPerHitBonus);
        }
    }
}