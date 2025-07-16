using Cysharp.Threading.Tasks;
using Module.SceneReference;

namespace Interface.Global.Scene;

public interface ISceneLoaderView
{
    public UniTask LoadScene(SceneReference sceneContext);
}

public interface INewSceneLoaderView
{
    public UniTask<SceneReleaseContext> LoadScene(string sceneContext);
    public UniTask ActivateAsync(SceneReleaseContext scene);
    public UniTask UnLoadScene(SceneReleaseContext releaseContext);
}