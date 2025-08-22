using System.Linq;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using Interface.View.Global;
using UnityEngine;

namespace Logic.Global.Scene;

public class LoadResourceScenesLogic : ILoadSceneResourcesLogic
{
    public LoadResourceScenesLogic
    (
        ISceneLoaderView sceneLoaderView,
        IResourceScenesModel sceneResourcesModel,
        IBlockingOperationModel blockingOperationModel
    )
    {
        SceneLoaderView = sceneLoaderView;
        SceneResourcesModel = sceneResourcesModel;
        BlockingOperationModel = blockingOperationModel;
    }

    private const string LoadContext = "Resource Scene Load";
    private const string UnLoadContext = "Resource Scene UnLoad";

    public async UniTask LoadResources()
    {
        using var handle = BlockingOperationModel.SpawnOperation(LoadContext);
        var scenes = SceneResourcesModel.GetResourceScenes();

        Debug.Log("load resource scene:\n" + string.Join("\n", scenes));
        for (int i = 0; i < scenes.Count; i++)
        {
            var scene = scenes[i]!;
            var releaseContext = await SceneLoaderView.LoadScene(scene);
            await SceneLoaderView.ActivateAsync(releaseContext);
            SceneResourcesModel.PushReleaseContext(releaseContext);
        }
    }

    public async UniTask UnLoadResources()
    {
        using var handle = BlockingOperationModel.SpawnOperation(UnLoadContext);
        var contexts = SceneResourcesModel.GetSceneReleaseContexts();
        Debug.Log("Unload resource scene:\n" + string.Join("\n", contexts.Select(x => x.ScenePath)));
        for (int i = 0; i < contexts.Count; i++)
        {
            var context = contexts[i];
            await SceneLoaderView.UnLoadScene(context);
        }
    }

    private ISceneLoaderView SceneLoaderView { get; }
    private IResourceScenesModel SceneResourcesModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}