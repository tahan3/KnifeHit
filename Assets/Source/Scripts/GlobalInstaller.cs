using Source.Scripts.Currency;
using Source.Scripts.Data;
using Source.Scripts.Data.LevelData;
using Source.Scripts.Gameplay;
using Source.Scripts.Leaderboard;
using Source.Scripts.Level;
using Source.Scripts.Profile;
using Source.Scripts.Scene;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Source.Scripts.UI
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private MainConfig mainConfig;
        
        public override void InstallBindings()
        {
            Container.Bind<MainConfig>().FromInstance(mainConfig).AsSingle();
            
            Container.Bind<ExpHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<CurrencyHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<MissionsHandler>().FromNew().AsSingle().NonLazy();
        }
    }
}