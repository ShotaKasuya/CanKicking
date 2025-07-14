using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class NewSceneLoaderView : INewSceneLoaderView
    {
        public async UniTask<SceneReleaseContext> LoadScene(string sceneContext)
        {
            var task = Addressables.LoadSceneAsync(sceneContext, LoadSceneMode.Additive).Task;
            var instance = await task.AsUniTask();
            await instance.ActivateAsync().ToUniTask();

            return new SceneReleaseContext(SceneType.Addressable, instance, instance.Scene.name);
        }

        public async UniTask UnLoadScene(SceneReleaseContext sceneInstance)
        {
            await Addressables.UnloadSceneAsync(sceneInstance.SceneInstance).Task;
        }
    }
}