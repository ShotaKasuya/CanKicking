using Cysharp.Threading.Tasks;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace ModuleExtension.SceneReferenceExtension
{
    public static class Extension
    {
        public static async UniTask LoadAsync(this SceneReference sceneReference)
        {
            switch (sceneReference.Type)
            {
                case SceneType.SceneManager:
                    await SceneManager.LoadSceneAsync(sceneReference.SceneName, LoadSceneMode.Single).ToUniTask();
                    break;
                case SceneType.Addressable:
                    var handle = Addressables.LoadSceneAsync(sceneReference.SceneName);
                    await handle.Task.AsUniTask();
                    if (handle.Status != AsyncOperationStatus.Succeeded)
                    {
                        Debug.LogError($"Failed to load scene at address: {sceneReference.SceneName}");
                    }
                    break;
            }
        }
    }
}
