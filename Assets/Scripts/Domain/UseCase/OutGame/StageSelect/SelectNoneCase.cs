using Domain.IPresenter.OutGame.StageSelect;
using Domain.IRepository.OutGame;
using Domain.IUseCase.OutGame;
using Module.Option;
using Module.StateMachine;
using Structure.OutGame;

namespace Domain.UseCase.OutGame.StageSelect
{
    public class SelectNoneCase : StageSelectStateBehaviourBase
    {
        public SelectNoneCase
        (
            ISelectedStagePresenter selectedStagePresenter,
            ISelectedStageRepository selectedStageRepository,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.None, stateEntity)
        {
            SelectedStagePresenter = selectedStagePresenter;
            SelectedStageRepository = selectedStageRepository;
        }

        public override void OnEnter()
        {
            SelectedStagePresenter.SelectEvent += OnSelect;
        }

        public override void OnExit()
        {
            SelectedStagePresenter.SelectEvent -= OnSelect;
        }

        private void OnSelect(Option<string> selectedStage)
        {
            if (!selectedStage.TryGetValue(out var stage))
            {
                return;
            }
            
            SelectedStageRepository.SetSelectedStage(stage);
            
            StateEntity.ChangeState(StageSelectStateType.Some);
        }
        
        private ISelectedStagePresenter SelectedStagePresenter { get; }
        private ISelectedStageRepository SelectedStageRepository { get; }
    }
}