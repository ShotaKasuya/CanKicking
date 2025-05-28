using Adapter.IView.OutGame.StageSelect;
using Adapter.IView.Scene;
using Domain.IRepository.OutGame;
using Module.Option;
using Module.StateMachine;
using R3;
using Structure.OutGame;
using VContainer.Unity;

namespace Domain.Controller.OutGame.StageSelect
{
    public class SomeStateController : StageSelectStateBehaviourBase, IStartable
    {
        public SomeStateController
        (
            ISceneLoadView sceneLoadView,
            ISelectedStageView selectedStageView,
            ISelectedStageTextView selectedStageTextView,
            ISelectedStageRepository selectedStageRepository,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.Some, stateEntity)
        {
            SceneLoadView = sceneLoadView;
            SelectedStageView = selectedStageView;
            SelectedStageTextView = selectedStageTextView;
            SelectedStageRepository = selectedStageRepository;

            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            SelectedStageView.SelectEvent
                .Where(_ => IsInState())
                .Subscribe(x => OnSelect(x))
                .AddTo(CompositeDisposable);
        }
        

        private void OnSelect(Option<string> selectedStage)
        {
            var prevSelect = SelectedStageRepository.SelectedStage;
            if (!selectedStage.TryGetValue(out var stage))
            {
                SelectedStageTextView.ResetStage();
                StateEntity.ChangeState(StageSelectStateType.None);
                return;
            }

            if (stage != prevSelect)
            {
                SelectedStageTextView.SetStage(stage);
                SelectedStageRepository.SetSelectedStage(stage);
                return;
            }

            SceneLoadView.Load(prevSelect);
        }

        private CompositeDisposable CompositeDisposable { get; }
        private ISelectedStageView SelectedStageView { get; }
        private ISelectedStageTextView SelectedStageTextView { get; }
        private ISceneLoadView SceneLoadView { get; }
        private ISelectedStageRepository SelectedStageRepository { get; }
    }
}