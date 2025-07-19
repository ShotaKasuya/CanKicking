using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using VContainer.Unity;

namespace Logic.Global.Scene;

public class LoadSceneResourcesLogic : ILoadSceneResourcesLogic
{
    public LoadSceneResourcesLogic
    (
        LifetimeScope lifetimeScope,
        INewSceneLoaderView sceneLoaderView,
        ISceneResourcesModel sceneResourcesModel,
        IBlockingOperationModel blockingOperationModel
    )
    {
        ParentScope = lifetimeScope;
        SceneLoaderView = sceneLoaderView;
        SceneResourcesModel = sceneResourcesModel;
        BlockingOperationModel = blockingOperationModel;
    }

    private const string LoadContext = "Scene Load";
    private const string UnLoadContext = "Scene UnLoad";

    public async UniTask LoadResources()
    {
        var operation = BlockingOperationModel.SpawnOperation(LoadContext);
        using (LifetimeScope.EnqueueParent(ParentScope))
        {
            var scenes = SceneResourcesModel.GetSceneResources();
            for (int i = 0; i < scenes.Count; i++)
            {
                var scene = scenes[i]!;
                var releaseContext = await SceneLoaderView.LoadScene(scene);
                SceneResourcesModel.PushReleaseContext(releaseContext);
            }
        }

        operation.Release();
    }

    public async UniTask UnLoadResources()
    {
        var operation = BlockingOperationModel.SpawnOperation(UnLoadContext);
        var contexts = SceneResourcesModel.GetSceneReleaseContexts();
        for (int i = 0; i < contexts.Count; i++)
        {
            var context = contexts[i];
            await SceneLoaderView.UnLoadScene(context);
        }

        operation.Release();
    }

    private LifetimeScope ParentScope { get; }
    private INewSceneLoaderView SceneLoaderView { get; }
    private ISceneResourcesModel SceneResourcesModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}