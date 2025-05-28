using R3;
using Structure.Scene;

namespace Adapter.IView.InGame.UI
{
    public interface ISceneChangeEventView
    {
        public Observable<SceneType> Performed { get; }
    }
}