using System;
using Interface.Global.Scene;
using Interface.OutGame.StageSelect;
using Module.Option;
using Module.SceneReference;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Domain.Controller.OutGame.StageSelect
{
    public class SomeStateController : StageSelectStateBehaviourBase, IStartable, IDisposable
    {
        public SomeStateController
        (
            ISceneLoaderView sceneLoaderView,
            IStageSelectionView stageSelectionView,
            ISelectedStageView selectedStageView,
            ISelectedStageModel selectedStageModel,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.Some, stateEntity)
        {
            SceneLoaderView = sceneLoaderView;
            StageSelectionView = stageSelectionView;
            SelectedStageView = selectedStageView;
            SelectedStageModel = selectedStageModel;
        
            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            StageSelectionView.SelectEvent
                .Where(_ => IsInState())
                .Subscribe(OnSelect)
                .AddTo(CompositeDisposable);
        }
        
        public override void OnEnter()
        {
            var stage = SelectedStageModel.SelectedStage;
            SelectedStageView.ShowStage(stage);
        }
        
        private void OnSelect(Option<SceneReference> selectedStage)
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
        
            SceneLoaderView.LoadScene(prevSelect);
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private ISceneLoaderView SceneLoaderView { get; }
        private IStageSelectionView StageSelectionView { get; }
        private ISelectedStageView SelectedStageView { get; }
        private ISelectedStageModel SelectedStageModel { get; }
        
        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}