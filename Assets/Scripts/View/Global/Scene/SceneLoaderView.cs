using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class SceneLoaderView : ISceneLoaderView
    {
        public async UniTask<SceneContext> LoadScene(string scenePath)
        {
            SceneType type;
            SceneInstance instance;
            AsyncOperation operation = null;

            var locationsHandle = Addressables.LoadResourceLocationsAsync(scenePath);
            await locationsHandle.Task;

            if (locationsHandle.Status == AsyncOperationStatus.Succeeded && locationsHandle.Result.Count > 0)
            {
                // アセットが存在する
                var handle = Addressables.LoadSceneAsync(scenePath, LoadSceneMode.Additive, false);
                instance = await handle.Task.AsUniTask();
                type = SceneType.Addressable;
            }
            else
            {
                // アセットが存在しない
                operation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
                operation!.allowSceneActivation = false;
                await operation.ToUniTask();
                instance = default;
                type = SceneType.SceneManager;
            }

            return new SceneContext(type, instance, operation, scenePath);
        }

        public async UniTask ActivateAsync(SceneContext scene)
        {
            if (scene.Type == SceneType.SceneManager)
            {
                scene.Operation.allowSceneActivation = true;
            }
            else if (scene.Type == SceneType.Addressable)
            {
                await scene.SceneInstance.ActivateAsync().ToUniTask();
            }

            var loadedScene = SceneManager.GetSceneByPath(scene.ScenePath);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                SceneManager.SetActiveScene(loadedScene);
            }
            else
            {
                Debug.LogWarning($"シーン {scene.ScenePath} は有効でないかロードされていません。");
            }
        }

        public async UniTask UnLoadScene(SceneContext sceneInstance)
        {
            if (sceneInstance.Type == SceneType.SceneManager)
            {
                await SceneManager.UnloadSceneAsync(sceneInstance.ScenePath);
            }
            else if (sceneInstance.Type == SceneType.Addressable)
            {
                await Addressables.UnloadSceneAsync(sceneInstance.SceneInstance).Task;
            }
        }
    }
}