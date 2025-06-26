using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using UnityEngine.SceneManagement;

namespace View.Global.Scene
{
    public class SceneLoaderView: ISceneLoaderView
    {
        public UniTask LoadScene(SceneReference sceneReference)
        {
            SceneManager.LoadScene(sceneReference.SceneName);
            return UniTask.CompletedTask;
        }
    }
}