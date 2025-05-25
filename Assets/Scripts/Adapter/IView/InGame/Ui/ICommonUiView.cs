using R3;
using Structure.Scene;

namespace Adapter.IView.InGame.UI
{
    public interface IStageSelectButtonView
    {
        public Observable<SceneType> Performed { get; }
    }

    public interface IReStartButtonView
    {
        public Observable<SceneType> Performed { get; }
    }
}