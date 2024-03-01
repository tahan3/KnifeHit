using Source.Scripts.Profile;
using Source.Scripts.Scene;
using UnityEngine;
using Zenject;

namespace Source.Scripts.Login
{
    public class LoginInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var login = new PlayFabLogin();
            login.OnLogin += OnLogin;
            login.OnError += OnError;

            Container.Bind<ILoginHandler<string>>().FromInstance(login).AsSingle();
            
            login.Login();
        }
        
        public void OnLogin()
        {
            Container.Bind<IProfileHandler>().To<PlayFabProfileHandler>().FromNew().AsSingle().NonLazy();
            SceneLoader.LoadScene("MainMenu");
        }

        public void OnError()
        {
            Container.Bind<IProfileHandler>().To<PlayFabProfileHandler>().FromNew().AsSingle().NonLazy();
            SceneLoader.LoadScene("MainMenu");
        }
    }
}