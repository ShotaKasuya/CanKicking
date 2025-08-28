using Cysharp.Threading.Tasks;
using Interface.View.Global;
using R3;
using VContainer.Unity;

namespace Controller.Global.UserInterface;

public class TouchUiController : IStartable
{
    public TouchUiController
    (
        ITouchView touchView,
        ITouchPositionUiView touchPositionUiView
    )
    {
        TouchView = touchView;
        TouchPositionUiView = touchPositionUiView;
        CompositeDisposable = new CompositeDisposable();
    }

    public void Start()
    {
        TouchView.TouchEvent
            .SubscribeAwait(this, (argument, controller, _) => controller.OnTouch(argument))
            .AddTo(CompositeDisposable);
        TouchView.TouchEndEvent
            .SubscribeAwait(this, (_, controller, _) => controller.OnTouchRelease())
            .AddTo(CompositeDisposable);
    }

    private async UniTask OnTouch(TouchStartEventArgument touchStartEvent)
    {
        await TouchPositionUiView.FadeIn(touchStartEvent.TouchPosition);
    }

    private async UniTask OnTouchRelease()
    {
        await TouchPositionUiView.FadeOut();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private ITouchPositionUiView TouchPositionUiView { get; }
}