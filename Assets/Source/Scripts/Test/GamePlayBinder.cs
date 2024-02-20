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
        private MainEventsHandler _mainEvents;
        private GameOverHandler _gameOverHandler;

        private KnifesPerRoundCounter _knifesPerRoundCounter;

        private MultiplierHandler _multiplierHandler;
        
        [Inject] private MissionsHandler _missionsHandler;
        
        public override void InstallBindings()
        {
            Container.Bind<ILeaderboardHandler>().To<PlayFabLeaderboardHandler>().FromNew().AsSingle().NonLazy();
            
            _mainEvents = new MainEventsHandler();
            _knifesPerRoundCounter = new KnifesPerRoundCounter();
            _multiplierHandler = new MultiplierHandler(1f, 1.5f, 0.1f);

            _gameOverHandler = new GameOverHandler(_knifesPerRoundCounter, _missionsHandler);
            
            Container.Bind<MainEventsHandler>().FromInstance(_mainEvents).AsSingle();
            Container.Bind<LevelConfig>().FromInstance(_missionsHandler.Mission.stages[_missionsHandler.Stage].levels[_missionsHandler.Level]).AsSingle();
            Container.Bind<StageConfig>().FromInstance(_missionsHandler.Mission.stages[_missionsHandler.Stage]).AsSingle();
            Container.Bind<KnifesPerRoundCounter>().FromInstance(_knifesPerRoundCounter).AsSingle();
            Container.Bind<GameOverHandler>().FromInstance(_gameOverHandler).AsSingle();
            Container.Bind<MultiplierHandler>().FromInstance(_multiplierHandler).AsSingle();

            _mainEvents.OnKnifeHitAim += () =>
                _missionsHandler.PointsCounter.CounterNumber.Value += (int)(100 * _multiplierHandler.Multiplier.Value);
            _mainEvents.OnKnifeHitAim += _multiplierHandler.IncreaseMultiplier;
            _mainEvents.OnKnifeHitAim += () => _knifesPerRoundCounter.CounterNumber.Value++;

            _mainEvents.OnKnifeEjected += _multiplierHandler.SetDefaultMultiplier;
        }
    }
}