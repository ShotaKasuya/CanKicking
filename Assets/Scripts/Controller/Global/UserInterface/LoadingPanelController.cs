using System;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Interface.Global.Utility;
using Interface.View.Global;
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
        ITimeScaleModel timeScaleModel,
        CompositeDisposable compositeDisposable
    )
    {
        LoadingPanelView = loadingPanelView;
        SceneLoadEventModel = sceneLoadEventModel;
        BlockingOperationModel = blockingOperationModel;
        TimeScaleModel = timeScaleModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        SceneLoadEventModel.StartLoadScene
            .Subscribe(this, (_, controller) => controller.FadeInPanel().Forget())
            .AddTo(CompositeDisposable);
        SceneLoadEventModel.EndLoadScene
            .Subscribe(this, (_, controller) => controller.FadeOutPanel().Forget())
            .AddTo(CompositeDisposable);
    }

    private const string FadeInContext = "Fade in loading panel";
    private const string FadeOutContext = "Fade out loading panel";

    private async UniTask FadeInPanel()
    {
        using var handle = BlockingOperationModel.SpawnOperation(FadeInContext);

        TimeScaleModel.Reset();
        try
        {
            await LoadingPanelView.ShowPanel();
        }
        catch (Exception e) when (e is not OperationCanceledException)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private async UniTask FadeOutPanel()
    {
        using var handle = BlockingOperationModel.SpawnOperation(FadeOutContext);

        await LoadingPanelView.HidePanel();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ILoadingPanelView LoadingPanelView { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
    private ITimeScaleModel TimeScaleModel { get; }
}