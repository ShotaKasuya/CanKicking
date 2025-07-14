using Cysharp.Threading.Tasks;
using Module.SceneReference;
using R3;

namespace Interface.Global.Scene;

public interface ISceneLoaderView
{
    public UniTask LoadScene(SceneReference sceneContext);
}
public interface INewSceneLoaderView
{
    public UniTask<SceneReleaseContext> LoadScene(string sceneContext);
    public UniTask UnLoadScene(SceneReleaseContext releaseContext);
}

public interface ISceneLoadEventView
{
    public Observable<Unit> BeforeSceneLoad { get; }
    public Observable<Unit> BeforeNextSceneActivate { get; }
    public Observable<Unit> BeforeSceneUnLoad { get; }
    public Observable<Unit> AfterSceneUnLoad { get; }
}
