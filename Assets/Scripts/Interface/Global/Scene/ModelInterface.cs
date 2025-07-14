using Module.SceneReference;

namespace Interface.Global.Scene;

public interface IPrimarySceneModel
{
    public SceneReleaseContext ToggleCurrentScene(SceneReleaseContext sceneInstance);
}