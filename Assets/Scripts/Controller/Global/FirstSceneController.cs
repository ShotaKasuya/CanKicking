using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.Global;
using Interface.Model.OutGame;
using Interface.View.Global;
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
        ILoadView<UserState> loadView,
        IPrimarySceneModel primarySceneModel,
        IEntrySceneModel entrySceneModel
    )
    {
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        LoadView = loadView;
        PrimarySceneModel = primarySceneModel;
        EntrySceneModel = entrySceneModel;
    }

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        PrimarySceneModel.ToggleCurrentScene(SceneContext.SceneManagerContext(
            null,
            SceneManager.GetActiveScene().path
        ));

        var userState = await LoadView.Load();

        if (userState.GameState == GameState.Tutorial)
        {
            await LoadPrimarySceneLogic.ChangeScene(EntrySceneModel.TutorialScene);
            return;
        }

        await LoadPrimarySceneLogic.ChangeScene(EntrySceneModel.TitleScene);
    }

    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private ILoadView<UserState> LoadView { get; }
    private IPrimarySceneModel PrimarySceneModel { get; }
    private IEntrySceneModel EntrySceneModel { get; }
}