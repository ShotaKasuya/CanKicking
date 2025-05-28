using Structure.Scene;

namespace Adapter.IView.Scene
{
    public interface ISceneLoadView
    {
        public void Load(SceneType sceneType);
        public void Load(string sceneName);
    }
}