using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using R3;
using Structure.Utility.Extension;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Controller.Global.Scene;

public class ResourceSceneController : IInitializable
{
    public ResourceSceneController
    (
        LifetimeScope parentLifetimeScope,
        ILoadSceneResourcesLogic loadResourcesSceneLogic,
        ISceneLoadEventModel sceneLoadEventModel,
        IResourceScenesModel resourceScenesModel,
        IBlockingOperationModel blockingOperationModel,
        CompositeDisposable compositeDisposable
    )
    {
        ParentLifetimeScope = parentLifetimeScope;
        LoadResourcesSceneLogic = loadResourcesSceneLogic;
        SceneLoadEventModel = sceneLoadEventModel;
        ResourceScenesModel = resourceScenesModel;
        BlockingOperationModel = blockingOperationModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Initialize()
    {
        SceneLoadEventModel.AfterSceneUnLoad
            .Subscribe(this, (_, controller) => controller.BuildAndLoadResources().Forget())
            .AddTo(CompositeDisposable);
        SceneLoadEventModel.BeforeSceneLoad
            .Subscribe(this, (_, controller) => controller.UnLoadResourceScene().Forget())
            .AddTo(CompositeDisposable);
    }

    private const string BuildLoadResourcesContext = "Build load resources";
    private const string UnLoadResourcesContext = "UnLoad Resource Scenes";

    private async UniTask BuildAndLoadResources()
    {
        using var handle = BlockingOperationModel.SpawnOperation(BuildLoadResourcesContext);

        Debug.Log($"Primary LifetimeScope: {ParentLifetimeScope.name}");
        await LoadResourcesSceneLogic.LoadResources();

        using var _ = LifetimeScope.EnqueueParent(ParentLifetimeScope);

        var sceneContexts = ResourceScenesModel.GetResourceScenes();
        for (int i = 0; i < sceneContexts.Count; i++)
        {
            var scene = SceneManager.GetSceneByPath(sceneContexts[i]);
            var rootGameObjects = scene.GetRootGameObjects()!;

            for (int j = 0; j < rootGameObjects.Length; j++)
            {
                var lifetimeScopes = rootGameObjects[j].GetComponentsInChildren<LifetimeScope>()!;

                foreach (var scope in lifetimeScopes)
                {
                    await scope.BuildOnThreadPool();
                }
            }
        }
    }

    private async UniTask UnLoadResourceScene()
    {
        using var _ = BlockingOperationModel.SpawnOperation(UnLoadResourcesContext);

        await LoadResourcesSceneLogic.UnLoadResources();
    }

    private LifetimeScope ParentLifetimeScope { get; }
    private CompositeDisposable CompositeDisposable { get; }
    private ILoadSceneResourcesLogic LoadResourcesSceneLogic { get; }
    private ISceneLoadEventModel SceneLoadEventModel { get; }
    private IResourceScenesModel ResourceScenesModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
}