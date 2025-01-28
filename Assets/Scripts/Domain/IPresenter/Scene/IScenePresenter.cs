using DataUtil.Scene;

namespace Domain.IPresenter.Scene
{
    public interface IScenePresenter
    {
        public void Load(SceneType sceneType);
    }
}