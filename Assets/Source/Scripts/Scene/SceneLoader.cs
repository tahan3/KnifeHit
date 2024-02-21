using System;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Scene
{
    public class SceneLoader
    {
        public static async UniTaskVoid LoadScene(string name, Action callback = null)
        {
            var currentScene = SceneManager.GetActiveScene();
            var loading = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
            
            await loading;

            //var unloading = SceneManager.UnloadSceneAsync(currentScene);

            //await unloading;
            
            callback?.Invoke();
        }
    }
}