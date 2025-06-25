using Cysharp.Threading.Tasks;
using R3;

namespace Interface.InGame.UserInterface
{
    public interface INormalUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IStopButtonView
    {
        public Observable<Unit> Performed { get; }
    }

    public interface IGoalEventView
    {
        public Observable<Unit> Performed { get; }
    }

    public interface IHeightUiView
    {
        public void SetHeight(float height);
    }
}