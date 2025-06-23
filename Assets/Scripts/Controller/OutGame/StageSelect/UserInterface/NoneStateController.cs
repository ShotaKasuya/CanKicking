using System;
using Cysharp.Threading.Tasks;
using Interface.OutGame.StageSelect;
using Module.Option;
using Module.SceneReference;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Domain.Controller.OutGame.StageSelect
{
    public class NoneStateController : StageSelectStateBehaviourBase, IStartable, IDisposable
    {
        public NoneStateController
        (
            IStageSelectionView stageSelectionView,
            ISelectedStageView selectedStageView,
            ISelectedStageModel selectedStageModel,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.None, stateEntity)
        {
            StageSelectionView = stageSelectionView;
            SelectedStageView = selectedStageView;
            SelectedStageModel = selectedStageModel;
        
            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            StageSelectionView.SelectEvent
                .Where(_ => IsInState())
                .Subscribe(x => OnSelect(x).Forget())
                .AddTo(CompositeDisposable);
        }
        
        public override void OnEnter()
        {
            SelectedStageView.Reset();
        }
        
        private async UniTask OnSelect(Option<SceneReference> selectedStage)
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
        
        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}