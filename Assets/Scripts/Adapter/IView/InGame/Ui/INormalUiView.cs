using Cysharp.Threading.Tasks;
using R3;

namespace Adapter.IView.InGame.UI
{
    public interface INormalUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IStopEventView
    {
        public Observable<Unit> OnPerformed { get; }
    }

    public interface IGoalEventView
    {
        public Observable<Unit> OnPerformed { get; }
    }

    public interface IHeightUiView
    {
        public void SetHeight(float height);
    }
}