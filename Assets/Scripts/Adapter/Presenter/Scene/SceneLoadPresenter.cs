using Adapter.IView.Scene;
using Domain.IPresenter.Scene;
using Structure.Scene;

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
        
        public void Load(string sceneName)
        {
            SceneLoadView.Load(sceneName);
        }
        
        private ISceneLoadView SceneLoadView { get; }
    }
}