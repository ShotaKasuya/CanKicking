using R3;
using Structure.Scene;

namespace Adapter.IView.InGame.Ui
{
    public interface ISceneChangeEventView
    {
        public Observable<SceneType> Performed { get; }
    }
}