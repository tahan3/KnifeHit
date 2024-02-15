using Source.Scripts.Data.LevelData;
using Source.Scripts.Gameplay;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Source.Scripts.UI
{
    public class GlobalInstaller : MonoInstaller
    {
        private MissionsHandler _missionsHandler;
        
        public override void InstallBindings()
        {
            _missionsHandler = new MissionsHandler();

            Container.Bind<MissionsHandler>().FromInstance(_missionsHandler).AsSingle().NonLazy();

            SceneManager.LoadScene("MainMenu");
        }
    }
}