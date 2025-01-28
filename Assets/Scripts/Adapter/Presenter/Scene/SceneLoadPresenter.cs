using Adapter.IView.Scene;
using DataUtil.Scene;
using Domain.IPresenter.Scene;

namespace Adapter.Presenter.Scene
{
    public class SceneLoadPresenter: IScenePresenter
    {
        public SceneLoadPresenter
        (
            ISceneLoadView sceneLoadView
        )
        {
            SceneLoadView = sceneLoadView;
        }
        
        public void Load(SceneType sceneType)
        {
            SceneLoadView.Load(sceneType.ToSceneName());
        }
        
        private ISceneLoadView SceneLoadView { get; }
    }
}