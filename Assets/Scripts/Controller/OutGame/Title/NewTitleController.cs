using Cysharp.Threading.Tasks;
using Interface.Global.Input;
using Interface.Global.Scene;
using Interface.OutGame.Title;
using R3;
using VContainer.Unity;

namespace Controller.OutGame.Title;

public class NewTitleController : IStartable
{
    public NewTitleController
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
            .Subscribe(this, (_, controller) => controller.StartGame())
            .AddTo(CompositeDisposable);
    }

    private void StartGame()
    {
        var scene = StartSceneModel.GetStartSceneName();
        LoadPrimarySceneLogic.ChangeScene(scene).Forget();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ITouchView TouchView { get; }
    private IStartSceneModel StartSceneModel { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
}