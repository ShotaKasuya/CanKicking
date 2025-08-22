using System.Collections.Generic;
using Module.SceneReference;
using Module.SceneReference.Runtime;
using R3;

namespace Interface.Global.Scene;

public interface IPrimarySceneModel
{
    public SceneContext ToggleCurrentScene(SceneContext sceneInstance);
    public SceneContext GetCurrentSceneContext { get; }
}

public interface ISceneLoadEventModel
{
    public Observable<Unit> StartLoadScene { get; }
    public Observable<Unit> BeforeSceneLoad { get; }
    public Observable<Unit> AfterSceneLoad { get; }
    public Observable<Unit> BeforeNextSceneActivate { get; }
    public Observable<Unit> AfterNextSceneActivate { get; }
    public Observable<Unit> BeforeSceneUnLoad { get; }
    public Observable<Unit> AfterSceneUnLoad { get; }
    public Observable<Unit> EndLoadScene { get; }
}

public interface ISceneLoadSubjectModel
{
    public void InvokeStartLoadScene();
    public void InvokeBeforeSceneLoad();
    public void InvokeAfterSceneLoad();
    public void InvokeBeforeNextSceneActivate();
    public void InvokeAfterNextSceneActivate();
    public void InvokeBeforeSceneUnLoad();
    public void InvokeAfterSceneUnLoad();
    public void InvokeEndLoadScene();
}

public interface IResourceScenesModel
{
    public IReadOnlyList<string> GetResourceScenes();
    public void PushReleaseContext(SceneContext sceneContext);
    public IReadOnlyList<SceneContext> GetSceneReleaseContexts();
}