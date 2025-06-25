using Cysharp.Threading.Tasks;
using Module.SceneReference;
using R3;

namespace Interface.InGame.UserInterface
{
    public interface IStopUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }
    /// <summary>
    /// 停止状態から再開するボタンのView
    /// </summary>
    public interface IPlayButtonView
    {
        public Observable<Unit> Performed { get; }
    }

    public interface IScreenScaleSliderView
    {
        
    }
    public interface IStop_RestartButtonView
    {
        public Observable<SceneReference> Performed { get; }
    }

    public interface IStop_StageSelectButtonView
    {
        public Observable<SceneReference> Performed { get; }
    }
}