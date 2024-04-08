using Source.Scripts.Profile;
using Source.Scripts.SceneManagement;
using Zenject;

namespace Source.Scripts.Login
{
    public class LoginInstaller : Installer<LoginInstaller>
    {
        private readonly ILoginHandler<string> _loginHandler;

        [Inject] private SceneLoader _sceneLoader;
        
        public LoginInstaller(ILoginHandler<string> loginHandler)
        {
            _loginHandler = loginHandler;
        }

        public override void InstallBindings()
        {
            _loginHandler.OnLogin += OnLogin;
            _loginHandler.OnError += OnError;
            
            Container.Bind<ILoginHandler<string>>().FromInstance(_loginHandler).AsSingle();
            
            _loginHandler.Login();
        }
        
        public async void OnLogin()
        {
            Container.Bind<IProfileHandler>().To<PlayFabProfileHandler>().FromNew().AsSingle().NonLazy();
            await _sceneLoader.LoadScene(SceneType.MainMenu);
        }

        public async void OnError()
        {
            Container.Bind<IProfileHandler>().To<PlayFabProfileHandler/*LocalProfileHandler*/>().FromNew().AsSingle().NonLazy();
            await _sceneLoader.LoadScene(SceneType.MainMenu);
        }
    }
}