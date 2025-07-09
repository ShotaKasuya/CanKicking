using Cysharp.Threading.Tasks;
using Module.SceneReference;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace ModuleExtension.SceneReferenceExtension
{
    public static class Extension
    {
        public static async UniTask LoadAsync(this SceneReference sceneReference)
        {
            switch (sceneReference.SceneType)
            {
                case SceneType.Local:
                    await SceneManager.LoadSceneAsync(sceneReference.SceneName, LoadSceneMode.Single).ToUniTask();
                    break;
                case SceneType.Addressable:
                    await Addressables.LoadSceneAsync(sceneReference.SceneName);
                    break;
            }
        }
    
    }
}
