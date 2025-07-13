using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class SceneLoaderView : ISceneLoaderView
    {
        private SceneContext _prevScene;
        private SceneInstance _prevInstance;

        public async UniTask LoadScene(SceneReference sceneReference)
        {
            await InternalLoad(sceneReference);
        }

        private async UniTask InternalLoad(SceneReference sceneReference)
        {
            switch (sceneReference.Type)
            {
                case SceneType.SceneManager:
                    await SceneManager.LoadSceneAsync(sceneReference.SceneName).ToUniTask();
                    _prevScene = new SceneContext(
                        SceneType.SceneManager,
                        SceneManager.GetSceneByName(sceneReference.SceneName)
                    );
                    break;
                case SceneType.Addressable:
                    var task = Addressables.LoadSceneAsync(sceneReference.ScenePath).Task;
                    _prevInstance = await task.AsUniTask();
                    _prevScene = new SceneContext(SceneType.Addressable, _prevInstance.Scene);
                    break;
            }
        }
    }
}