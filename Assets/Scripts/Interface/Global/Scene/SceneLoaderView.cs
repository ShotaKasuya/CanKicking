using Cysharp.Threading.Tasks;
using Module.SceneReference;

namespace Interface.Global.Scene
{
    public interface ISceneLoaderView
    {
        public UniTask LoadScene(SceneReference sceneReference);
    }
}