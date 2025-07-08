using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;

namespace View.Global.Scene
{
    public class SceneLoaderView: ISceneLoaderView
    {
        public UniTask LoadScene(SceneReference sceneReference)
        {
            sceneReference.Load();
            return UniTask.CompletedTask;
        }
    }
}