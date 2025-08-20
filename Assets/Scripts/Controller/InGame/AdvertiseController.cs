using Interface.Global.Advertisement;
using Interface.Global.Scene;
using R3;
using VContainer.Unity;

namespace Controller.InGame;

public class AdvertiseController : IInitializable
{
    public AdvertiseController
    (
        IAdvertisementView advertisementView,
        ISceneLoadEventModel sceneLoadEventModel,
        CompositeDisposable compositeDisposable
    )
    {
        AdvertisementView = advertisementView;
        SceneLoadEventModel = sceneLoadEventModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Initialize()
    {
        SceneLoadEventModel.EndLoadScene
            .Subscribe(this, (_, controller) => controller.AdvertisementView.Spawn())
            .AddTo(CompositeDisposable);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IAdvertisementView AdvertisementView { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
}