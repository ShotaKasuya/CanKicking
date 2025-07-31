using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.OutGame.StageSelect;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect;

public class InitializeController : IAsyncStartable
{
    public InitializeController
    (
        IStageIconFactoryView stageIconFactoryView,
        IStageScenesModel stageScenesModel
    )
    {
        StageIconFactoryView = stageIconFactoryView;
        StageScenesModel = stageScenesModel;
    }

    public async UniTask StartAsync(CancellationToken cancellation = new CancellationToken())
    {
        var stageScenes = StageScenesModel.SceneList;

        await StageIconFactoryView.MakeIcons(stageScenes);
    }

    private IStageIconFactoryView StageIconFactoryView { get; }
    private IStageScenesModel StageScenesModel { get; }
}