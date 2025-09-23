using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.Global;
using Interface.Model.OutGame;
using Module.SceneReference.Runtime;
using Structure.Global;
using UnityEngine.SceneManagement;
using VContainer.Unity;

namespace Controller.Global;

public class FirstSceneController : IAsyncStartable
{
    public FirstSceneController
    (
        ILoadPrimarySceneLogic loadPrimarySceneLogic,
        IPrimarySceneModel primarySceneModel,
        IEntrySceneModel entrySceneModel,
        IGameStateModel gameStateModel
    )
    {
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        PrimarySceneModel = primarySceneModel;
        EntrySceneModel = entrySceneModel;
        GameStateModel = gameStateModel;
    }

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        PrimarySceneModel.ToggleCurrentScene(SceneContext.SceneManagerContext(
            null,
            SceneManager.GetActiveScene().path
        ));

        await GameStateModel.Initialize();
        var gameState = GameStateModel.GameState;

        if (gameState == GameState.Tutorial)
        {
            await LoadPrimarySceneLogic.ChangeScene(EntrySceneModel.TutorialScene);
            return;
        }

        await LoadPrimarySceneLogic.ChangeScene(EntrySceneModel.TitleScene);
    }

    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private IPrimarySceneModel PrimarySceneModel { get; }
    private IEntrySceneModel EntrySceneModel { get; }
    private IGameStateModel GameStateModel { get; }
}