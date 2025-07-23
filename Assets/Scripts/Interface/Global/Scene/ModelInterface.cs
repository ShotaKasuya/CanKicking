using System.Collections.Generic;
using Module.SceneReference;
using R3;

namespace Interface.Global.Scene;

public interface IPrimarySceneModel
{
    public SceneContext ToggleCurrentScene(SceneContext sceneInstance);
}

public interface ISceneLoadEventModel
{
    public Observable<Unit> BeforeSceneLoad { get; }
    public Observable<Unit> AfterSceneLoad { get; }
    public Observable<Unit> BeforeNextSceneActivate { get; }
    public Observable<Unit> AfterNextSceneActivate { get; }
    public Observable<Unit> BeforeSceneUnLoad { get; }
    public Observable<Unit> AfterSceneUnLoad { get; }

    public void InvokeBeforeSceneLoad();
    public void InvokeAfterSceneLoad();
    public void InvokeBeforeNextSceneActivate();
    public void InvokeAfterNextSceneActivate();
    public void InvokeBeforeSceneUnLoad();
    public void InvokeAfterSceneUnLoad();
}

public interface IResourceScenesModel
{
    public IReadOnlyList<string> GetResourceScenes();
    public void PushReleaseContext(SceneContext sceneContext);
    public IReadOnlyList<SceneContext> GetSceneReleaseContexts();
}