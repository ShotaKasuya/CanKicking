using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using Interface.View.Global;
using Module.SceneReference.Runtime;
using Structure.Utility.Extension;
using UnityEngine;
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
        ISceneLoadSubjectModel sceneLoadSubjectModel
    )
    {
        PrimarySceneModel = primarySceneModel;
        BlockingOperationModel = blockingOperationModel;
        SceneLoaderView = sceneLoaderView;
        SceneLoadSubjectModel = sceneLoadSubjectModel;
    }

    public async UniTask ChangeScene(string scenePath)
    {
        SceneLoadSubjectModel.InvokeStartLoadScene();
        await WaitBlock();
        var sceneInstance = await LoadScene(scenePath);

        await ActivateScene(sceneInstance);

        var prevSceneInstance = PrimarySceneModel.ToggleCurrentScene(sceneInstance);

        await UnLoadScene(prevSceneInstance);
        SceneLoadSubjectModel.InvokeEndLoadScene();
        await WaitBlock();
    }

    private async UniTask<SceneContext> LoadScene(string scenePath)
    {
        SceneLoadSubjectModel.InvokeBeforeSceneLoad();
        await WaitBlock();

        var sceneInstance = await SceneLoaderView.LoadScene(scenePath);

        SceneLoadSubjectModel.InvokeAfterSceneLoad();
        await WaitBlock();

        return sceneInstance;
    }

    private async UniTask ActivateScene(SceneContext sceneContext)
    {
        SceneLoadSubjectModel.InvokeBeforeNextSceneActivate();
        await WaitBlock();

        await SceneLoaderView.ActivateAsync(sceneContext);

        Debug.Log("Load primary scene\n" + sceneContext.ScenePath);
        var scene = SceneManager.GetSceneByPath(sceneContext.ScenePath);
        var rootGameObjects = scene.GetRootGameObjects()!;

        for (int j = 0; j < rootGameObjects.Length; j++)
        {
            var lifetimeScopes = rootGameObjects[j].GetComponentsInChildren<LifetimeScope>()!;

            foreach (var scope in lifetimeScopes)
            {
                Debug.Log($"scene: {scene.name}, lifetime scope: {scope.name}");
                await scope.BuildOnThreadPool();
            }
        }

        SceneLoadSubjectModel.InvokeAfterNextSceneActivate();
        await WaitBlock();
    }

    private async UniTask UnLoadScene(SceneContext sceneContext)
    {
        SceneLoadSubjectModel.InvokeBeforeSceneUnLoad();
        await WaitBlock();

        await SceneLoaderView.UnLoadScene(sceneContext);

        SceneLoadSubjectModel.InvokeAfterSceneUnLoad();
        await WaitBlock();
    }

    private async UniTask WaitBlock()
    {
        await UniTask.WaitWhile(BlockingOperationModel, model => model.IsAnyBlocked());
    }

    private IPrimarySceneModel PrimarySceneModel { get; }
    private IBlockingOperationModel BlockingOperationModel { get; }
    private ISceneLoaderView SceneLoaderView { get; }
    private ISceneLoadSubjectModel SceneLoadSubjectModel { get; }
}