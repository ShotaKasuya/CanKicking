using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.OutGame.StageSelect;
using Module.Option;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Controller.OutGame.StageSelect.UserInterface;

public class SomeStateController : StageSelectStateBehaviourBase, IStartable
{
    public SomeStateController
    (
        ILoadPrimarySceneLogic loadPrimarySceneLogic,
        IStageSelectionView stageSelectionView,
        ISelectedStageView selectedStageView,
        ISelectedStageModel selectedStageModel,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<StageSelectStateType> stateEntity
    ) : base(StageSelectStateType.Some, stateEntity)
    {
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        StageSelectionView = stageSelectionView;
        SelectedStageView = selectedStageView;
        SelectedStageModel = selectedStageModel;
        CompositeDisposable = compositeDisposable;
    }
    
    public void Start()
    {
        StageSelectionView.SelectEvent
            .Where(this, (_, controller) => controller.IsInState())
            .Subscribe(this, (option, controller) => controller.OnSelect(option))
            .AddTo(CompositeDisposable);
    }

    public override void OnEnter()
    {
        var stage = SelectedStageModel.SelectedStage;
        SelectedStageView.ShowStage(stage);
    }

    private void OnSelect(Option<string> selectedStage)
    {
        var prevSelect = SelectedStageModel.SelectedStage;
        if (!selectedStage.TryGetValue(out var stage))
        {
            StateEntity.ChangeState(StageSelectStateType.None);
            return;
        }

        if (stage != prevSelect)
        {
            SelectedStageModel.SetSelectedStage(stage);
            OnEnter();
            return;
        }

        LoadPrimarySceneLogic.ChangeScene(stage).Forget();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private IStageSelectionView StageSelectionView { get; }
    private ISelectedStageView SelectedStageView { get; }
    private ISelectedStageModel SelectedStageModel { get; }
}