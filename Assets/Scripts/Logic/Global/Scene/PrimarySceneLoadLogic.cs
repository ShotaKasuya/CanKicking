using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;

namespace Logic.Global.Scene;

public class PrimarySceneLoadLogic : ILoadScenePrimaryLogic
{
    public PrimarySceneLoadLogic
    (
        IPrimarySceneModel primarySceneModel,
        IBlockingOperationModel blockingOperationModel,
        INewSceneLoaderView sceneLoaderView,
        ISceneLoadEventModel sceneLoadEventModel
    )
    {
        PrimarySceneModel = primarySceneModel;
        BlockingOperationModel = blockingOperationModel;
        SceneLoaderView = sceneLoaderView;
        SceneLoadEventModel = sceneLoadEventModel;
    }

    public async UniTask ChangeScene(string scenePath)
    {
        SceneLoadEventModel.InvokeBeforeSceneLoad();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

        var sceneInstance = await SceneLoaderView.LoadScene(scenePath);
        SceneLoadEventModel.InvokeBeforeSceneUnLoad();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

        var prevSceneInstance = PrimarySceneModel.ToggleCurrentScene(sceneInstance);
        await SceneLoaderView.UnLoadScene(prevSceneInstance);
        SceneLoadEventModel.InvokeAfterSceneUnLoad();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

        SceneLoadEventModel.InvokeBeforeNextSceneActivate();
        await SceneLoaderView.ActivateAsync(sceneInstance);
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());
    }

    private IPrimarySceneModel PrimarySceneModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
    private INewSceneLoaderView SceneLoaderView { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
}