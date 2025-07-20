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
    public class NewSceneLoaderView : INewSceneLoaderView
    {
        public async UniTask<SceneReleaseContext> LoadScene(string scenePath)
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

            return new SceneReleaseContext(type, instance, operation, instance.Scene.name);
        }

        public async UniTask ActivateAsync(SceneReleaseContext scene)
        {
            if (scene.Type == SceneType.SceneManager)
            {
                scene.Operation.allowSceneActivation = true;
            }
            else if (scene.Type == SceneType.Addressable)
            {
                await scene.SceneInstance.ActivateAsync().ToUniTask();
            }

            // todo
            var loadedScene = SceneManager.GetSceneByName(scene.SceneName);
            if (loadedScene.IsValid() && loadedScene.isLoaded)
            {
                SceneManager.SetActiveScene(loadedScene);
            }
            else
            {
                Debug.LogWarning($"シーン {scene.SceneName} は有効でないかロードされていません。");
            }
        }

        public async UniTask UnLoadScene(SceneReleaseContext sceneInstance)
        {
            if (sceneInstance.Type == SceneType.SceneManager)
            {
                await SceneManager.UnloadSceneAsync(sceneInstance.SceneName);
            }
            else if (sceneInstance.Type == SceneType.Addressable)
            {
                await Addressables.UnloadSceneAsync(sceneInstance.SceneInstance).Task;
            }
        }
    }
}