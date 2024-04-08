using Cysharp.Threading.Tasks;
using Source.Scripts.SceneManagement;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Source.Scripts.Installers
{
    public class ScenesInstaller : IUniTaskAsyncInstaller
    {
        private readonly AssetReference _scenesConfig;

        public ScenesInstaller(AssetReference scenesConfig)
        {
            _scenesConfig = scenesConfig;
        }

        public async UniTask Install(DiContainer container)
        {
            var loading = await _scenesConfig.LoadAssetAsync<ScenesConfig>();

            container.Bind<SceneLoader>().FromNew().AsSingle().WithArguments(loading);
        }
    }
}