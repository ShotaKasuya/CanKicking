using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;

namespace Logic.Global.Scene;

public class LoadResourcesSceneLogic : ILoadResourcesSceneLogic
{
    public LoadResourcesSceneLogic
    (
        INewSceneLoaderView sceneLoaderView,
        IBlockingOperationModel blockingOperationModel
    )
    {
        SceneLoaderView = sceneLoaderView;
        BlockingOperationModel = blockingOperationModel;
    }

    private const string LoadContext = "Scene Load";
    private const string UnLoadContext = "Scene UnLoad";

    public async UniTask LoadResources(ISceneResourcesModel sceneResourcesModel)
    {
        var operation = BlockingOperationModel.SpawnOperation(LoadContext);
        var scenes = sceneResourcesModel.GetSceneResources();
        for (int i = 0; i < scenes.Count; i++)
        {
            var scene = scenes[i];
            var releaseContext = await SceneLoaderView.LoadScene(scene.Scene.name);
            sceneResourcesModel.PushReleaseContext(releaseContext);
        }

        operation.Release();
    }

    public async UniTask UnLoadResources(ISceneResourcesModel sceneResourcesModel)
    {
        var operation = BlockingOperationModel.SpawnOperation(UnLoadContext);
        var contexts = sceneResourcesModel.GetSceneReleaseContexts();
        for (int i = 0; i < contexts.Count; i++)
        {
            var context = contexts[i];
            await SceneLoaderView.UnLoadScene(context);
        }

        operation.Release();
    }

    private INewSceneLoaderView SceneLoaderView { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}