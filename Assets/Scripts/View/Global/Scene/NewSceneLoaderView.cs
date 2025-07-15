using System;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class NewSceneLoaderView : INewSceneLoaderView
    {
        public async UniTask<SceneReleaseContext> LoadScene(string sceneContext)
        {
            var handle = Addressables.LoadSceneAsync(sceneContext, LoadSceneMode.Additive, false);
            SceneType type;
            SceneInstance instance;
            AsyncOperation operation = null;
            try
            {
                instance = await handle.Task.AsUniTask();
                type = SceneType.Addressable;
            }
            catch (Exception _)
            {
                operation = SceneManager.LoadSceneAsync(sceneContext, LoadSceneMode.Additive);
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
        }

        public async UniTask UnLoadScene(SceneReleaseContext sceneInstance)
        {
            await Addressables.UnloadSceneAsync(sceneInstance.SceneInstance).Task;
        }
    }
}