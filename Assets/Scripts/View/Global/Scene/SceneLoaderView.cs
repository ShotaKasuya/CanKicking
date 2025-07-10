using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;

namespace View.Global.Scene
{
    public class SceneLoaderView: ISceneLoaderView
    {
        private SceneReference _prevScene;
        
        public UniTask LoadScene(SceneReference sceneReference)
        {
            _prevScene.UnLoad();
            sceneReference.Load();
            return UniTask.CompletedTask;
        }
    }
}