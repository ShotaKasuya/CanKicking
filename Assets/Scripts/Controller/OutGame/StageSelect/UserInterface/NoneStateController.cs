using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.OutGame.StageSelect;
using Module.Option.Runtime;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect.UserInterface;

public class NoneStateController : StageSelectStateBehaviourBase, IStartable
{
    public NoneStateController
    (
        IStageSelectionView stageSelectionView,
        ISelectedStageView selectedStageView,
        ISelectedStageModel selectedStageModel,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<StageSelectStateType> stateEntity
    ) : base(StageSelectStateType.None, stateEntity)
    {
        StageSelectionView = stageSelectionView;
        SelectedStageView = selectedStageView;
        SelectedStageModel = selectedStageModel;
        CompositeDisposable = compositeDisposable;
    }

    public void Start()
    {
        StageSelectionView.SelectEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (option, controller) => controller.OnSelect(option).Forget())
            .AddTo(CompositeDisposable);
    }

    public override UniTask OnEnter(CancellationToken token)
    {
        SelectedStageView.Reset();
        return UniTask.CompletedTask;
    }

    private async UniTask OnSelect(Option<string> selectedStage)
    {
        if (!selectedStage.TryGetValue(out var stage))
        {
            return;
        }

        SelectedStageModel.SetSelectedStage(stage!);

        // FIXME: 待機せずに状態遷移すると、次の状態で同じイベントが起動する
        await UniTask.DelayFrame(1);
        StateEntity.ChangeState(StageSelectStateType.Some).Forget();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IStageSelectionView StageSelectionView { get; }
    private ISelectedStageView SelectedStageView { get; }
    private ISelectedStageModel SelectedStageModel { get; }
}