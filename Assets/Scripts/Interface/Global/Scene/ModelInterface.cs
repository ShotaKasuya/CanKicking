using Module.SceneReference;
using R3;

namespace Interface.Global.Scene;

public interface IPrimarySceneModel
{
    public SceneReleaseContext ToggleCurrentScene(SceneReleaseContext sceneInstance);
}

public interface ISceneLoadEventModel
{
    public Observable<Unit> BeforeSceneLoad { get; }
    public Observable<Unit> BeforeNextSceneActivate { get; }
    public Observable<Unit> BeforeSceneUnLoad { get; }
    public Observable<Unit> AfterSceneUnLoad { get; }

    public void InvokeBeforeSceneLoad();
    public void InvokeBeforeNextSceneActivate();
    public void InvokeBeforeSceneUnLoad();
    public void InvokeAfterSceneUnLoad();
}