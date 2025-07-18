using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;

namespace Logic.Global.Scene;

public class SceneResourcesLoadLogic : ILoadResourcesSceneLogic
{
    public SceneResourcesLoadLogic
    (
        INewSceneLoaderView sceneLoaderView,
        ISceneResourcesModel sceneResourcesModel,
        IBlockingOperationModel blockingOperationModel
    )
    {
        SceneLoaderView = sceneLoaderView;
        SceneResourcesModel = sceneResourcesModel;
        BlockingOperationModel = blockingOperationModel;
    }

    private const string LoadContext = "Scene Load";
    private const string UnLoadContext = "Scene UnLoad";

    public async UniTask LoadResources()
    {
        var operation = BlockingOperationModel.SpawnOperation(LoadContext);
        var scenes = SceneResourcesModel.GetSceneResources();
        for (int i = 0; i < scenes.Count; i++)
        {
            var scene = scenes[i]!;
            var releaseContext = await SceneLoaderView.LoadScene(scene);
            SceneResourcesModel.PushReleaseContext(releaseContext);
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

    private INewSceneLoaderView SceneLoaderView { get; }
    private ISceneResourcesModel SceneResourcesModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}