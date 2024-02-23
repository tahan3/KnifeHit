using Source.Scripts.Currency;
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
        public override void InstallBindings()
        {
            Container.Bind<ExpHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<CurrencyHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<MissionsHandler>().FromNew().AsSingle().NonLazy();
        }
    }
}