using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using R3;
using VContainer.Unity;

namespace Controller.Global.Scene;

public class SceneController : IInitializable
{
    public SceneController
    (
        ILoadSceneResourcesLogic loadResourcesSceneLogic,
        ISceneLoadEventModel sceneLoadEventModel,
        CompositeDisposable compositeDisposable
    )
    {
        LoadResourcesSceneLogic = loadResourcesSceneLogic;
        SceneLoadEventModel = sceneLoadEventModel;

        CompositeDisposable = compositeDisposable;
    }

    public void Initialize()
    {
        SceneLoadEventModel.BeforeSceneLoad
            .Subscribe(LoadResourcesSceneLogic, (_, logic) => logic.LoadResources().Forget())
            .AddTo(CompositeDisposable);
        SceneLoadEventModel.BeforeNextSceneActivate
            .Subscribe(LoadResourcesSceneLogic, (_, logic) => logic.UnLoadResources().Forget())
            .AddTo(CompositeDisposable);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ILoadSceneResourcesLogic LoadResourcesSceneLogic { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
}