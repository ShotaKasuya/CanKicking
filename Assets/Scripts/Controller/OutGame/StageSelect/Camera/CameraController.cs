using Interface.Global.Input;
using Interface.Global.Screen;
using Interface.OutGame.StageSelect;
using UnityEngine;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect.Camera;

public class CameraController : ITickable
{
    public CameraController
    (
        ITouchView touchView,
        ICameraPositionView cameraPositionView,
        IScreenScaleModel screenScaleModel
    )
    {
        TouchView = touchView;
        CameraPositionView = cameraPositionView;
        ScreenScaleModel = screenScaleModel;
    }

    public void Tick()
    {
        if (!TouchView.DraggingInfo.TryGetValue(out var info)) return;

        var screenY = ScreenScaleModel.Height;
        var moveTo = Vector2.down * info.CurrentFrameDelta / screenY;
        CameraPositionView.AddForce(moveTo);
    }

    private ITouchView TouchView { get; }
    private ICameraPositionView CameraPositionView { get; }
    private IScreenScaleModel ScreenScaleModel { get; }
}