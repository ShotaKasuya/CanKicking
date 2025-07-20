using Cysharp.Threading.Tasks;
using Interface.OutGame.StageSelect;
using Module.Option;
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

    public override void OnEnter()
    {
        SelectedStageView.Reset();
    }

    private async UniTask OnSelect(Option<string> selectedStage)
    {
        if (!selectedStage.TryGetValue(out var stage))
        {
            return;
        }

        SelectedStageModel.SetSelectedStage(stage);

        await UniTask.DelayFrame(1);
        StateEntity.ChangeState(StageSelectStateType.Some);
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IStageSelectionView StageSelectionView { get; }
    private ISelectedStageView SelectedStageView { get; }
    private ISelectedStageModel SelectedStageModel { get; }
}