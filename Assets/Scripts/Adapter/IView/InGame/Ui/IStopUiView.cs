using Cysharp.Threading.Tasks;
using R3;
using Structure.Scene;

namespace Adapter.IView.InGame.Ui
{
    public interface IStopUiView
    {
        public UniTask Hide();
        public UniTask Show();
    }

    public interface IPlayButtonView
    {
        public Observable<Unit> Performed { get; }
    }

    public interface IStopStateStageSelectButtonView
    {
        public Observable<SceneType> Performed { get; }
    }

    public interface IStopStateReStartButtonView
    {
        public Observable<SceneType> Performed { get; }
    }

    public interface IStopStateScreenScaleSliderView
    {
        public Observable<float> ChangeObservable { get; }
    }
}