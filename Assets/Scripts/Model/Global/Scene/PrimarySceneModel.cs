using Interface.Global.Scene;
using Module.SceneReference;

namespace Model.Global.Scene
{
    public class PrimarySceneModel : IPrimarySceneModel
    {
        private SceneReleaseContext _sceneReleaseContext;

        public SceneReleaseContext ToggleCurrentScene(SceneReleaseContext sceneInstance)
        {
            var tmp = _sceneReleaseContext;
            _sceneReleaseContext = sceneInstance;
            return tmp;
        }
    }
}