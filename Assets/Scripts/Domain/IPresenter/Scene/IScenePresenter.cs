using Structure.Scene;

namespace Domain.IPresenter.Scene
{
    public interface IScenePresenter
    {
        public void Load(SceneType sceneType);
    }
}