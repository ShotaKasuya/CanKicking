using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using Module.SceneReference.Runtime;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class SceneLoaderView : ISceneLoaderView
    {
        public async UniTask<SceneContext> LoadScene(string scenePath)
        {
            var locationsHandle = Addressables.LoadResourceLocationsAsync(scenePath);
            await locationsHandle.Task;

            if (locationsHandle.Status == AsyncOperationStatus.Succeeded && locationsHandle.Result.Count > 0)
            {
                // アセットが存在する
                var handle = Addressables.LoadSceneAsync(scenePath, LoadSceneMode.Additive, false);
                var instance = await handle.Task.AsUniTask();
                return SceneContext.AddressableContext(instance, scenePath);
            }
            else
            {
                // アセットが存在しない
                var operation = SceneManager.LoadSceneAsync(scenePath, LoadSceneMode.Additive);
                operation!.allowSceneActivation = false;
                await operation.ToUniTask();
                return SceneContext.SceneManagerContext(operation, scenePath);
            }
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
        }

        public void SetActiveScene(SceneContext scene)
        {
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

        public UnityEngine.SceneManagement.Scene CurrentScene => SceneManager.GetActiveScene();
    }
}