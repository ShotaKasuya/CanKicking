using Interface.Global.UserInterface;
using Interface.InGame.Player;
using VContainer.Unity;

namespace Controller.Global.UserInterface;

public class GlobalController: IStartable
{
    public GlobalController
    (
        ITouchView touchView,
        ITouchPositionUiView touchPositionUiView
    )
    {
        TouchView = touchView;
        TouchPositionUiView = touchPositionUiView;
    }
    
    public void Start()
    {
    }

    private ITouchView TouchView { get; }
    private ITouchPositionUiView TouchPositionUiView { get; }
}