using Interface.Model.Global;
using Module.SceneReference.Runtime;

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

        public SceneContext GetCurrentSceneContext => _sceneContext;
    }
}