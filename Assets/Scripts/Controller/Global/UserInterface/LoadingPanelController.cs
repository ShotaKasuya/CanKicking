using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.UserInterface;
using Interface.Global.Utility;
using R3;
using VContainer.Unity;

namespace Controller.Global.UserInterface;

public class LoadingPanelController : IStartable
{
    public LoadingPanelController
    (
        ILoadingPanelView loadingPanelView,
        ISceneLoadEventModel sceneLoadEventModel,
        IBlockingOperationModel blockingOperationModel,
        CompositeDisposable compositeDisposable
    )
    {
        LoadingPanelView = loadingPanelView;
        SceneLoadEventModel = sceneLoadEventModel;
        BlockingOperationModel = blockingOperationModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        SceneLoadEventModel.BeforeSceneLoad
            .Subscribe(this, (_, controller) => controller.FadeInPanel().Forget())
            .AddTo(CompositeDisposable);
        SceneLoadEventModel.AfterSceneUnLoad
            .Subscribe(this, (_, controller) => controller.FadeOutPanel().Forget())
            .AddTo(CompositeDisposable);
    }

    private const string FadeInContext = "Fade in loading panel";
    private const string FadeOutContext = "Fade out loading panel";

    private async UniTask FadeInPanel()
    {
        var handle = BlockingOperationModel.SpawnOperation(FadeInContext);
        await LoadingPanelView.ShowPanel();
        handle.Release();
    }

    private async UniTask FadeOutPanel()
    {
        var handle = BlockingOperationModel.SpawnOperation(FadeOutContext);
        await LoadingPanelView.HidePanel();
        handle.Release();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ILoadingPanelView LoadingPanelView { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}