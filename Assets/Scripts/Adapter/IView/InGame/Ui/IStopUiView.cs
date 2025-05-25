using Cysharp.Threading.Tasks;
using R3;
using Structure.Scene;

namespace Adapter.IView.InGame.UI
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

    public interface IStopStateStageSelectButtonView: IStageSelectButtonView
    {
    }

    public interface IStopStateReStartButtonView: IReStartButtonView
    {
    }
}