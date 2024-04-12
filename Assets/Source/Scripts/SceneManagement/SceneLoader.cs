using System;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Source.Scripts.SceneManagement
{
    public class SceneLoader
    {
        private readonly ScenesConfig _scenes;

        private SceneType _currentSceneType;
        
        public SceneLoader(ScenesConfig scenes)
        {
            _scenes = scenes;
        }

        public async UniTask LoadScene(SceneType sceneType, Action callback = null)
        {
            if (_scenes.TryGetValue(sceneType, out var sceneRef))
            {
                _currentSceneType = sceneType;

                await Addressables.LoadSceneAsync(sceneRef);
                
                Caching.ClearCache();
                Resources.UnloadUnusedAssets();
                GL.Clear(true, true, Color.green);
                GL.Flush();
                
                callback?.Invoke();
            }
        }

        public async UniTask ReloadCurrentScene(Action callback = null)
        {
            await LoadScene(_currentSceneType, callback);
        }
    }
}