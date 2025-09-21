using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.OutGame;
using Interface.View.Global;
using R3;
using VContainer.Unity;

namespace Controller.OutGame.Title;

public class TitleController : IStartable
{
    public TitleController
    (
        ITouchView touchView,
        IStartSceneModel startSceneModel,
        ILoadPrimarySceneLogic loadPrimarySceneLogic,
        CompositeDisposable compositeDisposable
    )
    {
        TouchView = touchView;
        StartSceneModel = startSceneModel;
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        TouchView.TouchEvent
            .SubscribeAwait(this, (_, controller, _) => controller.StartGame())
            .AddTo(CompositeDisposable);
    }

    private UniTask StartGame()
    {
        var scene = StartSceneModel.GetStartSceneName();
        return LoadPrimarySceneLogic.ChangeScene(scene);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IStartSceneModel StartSceneModel { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
}