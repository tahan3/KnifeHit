using Source.Scripts.ATT;
using Source.Scripts.Currency;
using Source.Scripts.Data;
using Source.Scripts.Gameplay;
using Source.Scripts.Installers;
using Source.Scripts.Level;
using Source.Scripts.Login;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Source.Scripts
{
    public class GlobalInstaller : MonoInstaller
    {
        [SerializeField] private MainConfig mainConfig;
        [SerializeField] private AssetReference scenes;
        
        public override async void InstallBindings()
        {
            ATTHandler.ShowATT();
            
            //Init scenes loader
            var scenesInstaller = new ScenesInstaller(scenes);
            await scenesInstaller.Install(Container);

            //Init player data initialization
            var loginInstaller = new LoginInstaller(/*!internet connection=>new LocalLoginHandler()*/new PlayFabLogin());
            Container.Inject(loginInstaller);
            loginInstaller.InstallBindings();
            
            Container.Bind<MainConfig>().FromInstance(mainConfig).AsSingle();
            Container.Bind<ExpHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<CurrencyHandler>().FromNew().AsSingle().NonLazy();
            Container.Bind<MissionsHandler>().FromNew().AsSingle().NonLazy();
        }
    }
}