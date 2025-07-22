using Interface.Global.Scene;
using Module.SceneReference;

namespace Model.Global.Scene
{
    public class PrimarySceneModel : IPrimarySceneModel
    {
        private SceneContext _sceneContext;

        public SceneContext ToggleCurrentScene(SceneContext sceneInstance)
        {
            var tmp = _sceneContext;
            _sceneContext = sceneInstance;
            return tmp;
        }
    }
}