using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using Module.SceneReference;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Logic.Global.Scene;

public class LoadPrimarySceneLogic : ILoadPrimarySceneLogic
{
    public LoadPrimarySceneLogic
    (
        IPrimarySceneModel primarySceneModel,
        IBlockingOperationModel blockingOperationModel,
        ISceneLoaderView sceneLoaderView,
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
        var sceneInstance = await LoadScene(scenePath);

        await ActivateScene(sceneInstance);

        var prevSceneInstance = PrimarySceneModel.ToggleCurrentScene(sceneInstance);

        await UnLoadScene(prevSceneInstance);
    }

    private async UniTask<SceneContext> LoadScene(string scenePath)
    {
        SceneLoadEventModel.InvokeBeforeSceneLoad();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

        var sceneInstance = await SceneLoaderView.LoadScene(scenePath);

        return sceneInstance;
    }

    private async UniTask ActivateScene(SceneContext sceneContext)
    {
        SceneLoadEventModel.InvokeBeforeNextSceneActivate();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

        await SceneLoaderView.ActivateAsync(sceneContext);
        
        var scene = SceneManager.GetSceneByPath(sceneContext.ScenePath);
        var rootGameObjects = scene.GetRootGameObjects()!;

        for (int j = 0; j < rootGameObjects.Length; j++)
        {
            var lifetimeScopes = rootGameObjects[j].GetComponentsInChildren<LifetimeScope>()!;

            foreach (var scope in lifetimeScopes)
            {
                await UniTask.RunOnThreadPool(scope.Build);
            }
        }

        SceneLoadEventModel.InvokeAfterNextSceneActivate();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());
    }

    private async UniTask UnLoadScene(SceneContext sceneContext)
    {
        SceneLoadEventModel.InvokeBeforeSceneUnLoad();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

        await SceneLoaderView.UnLoadScene(sceneContext);

        SceneLoadEventModel.InvokeAfterSceneUnLoad();
        await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());
    }

    private IPrimarySceneModel PrimarySceneModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
    private ISceneLoaderView SceneLoaderView { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
}