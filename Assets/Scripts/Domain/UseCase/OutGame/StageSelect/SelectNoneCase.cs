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
            IPlayerStageSelectionPresenter playerStageSelectionPresenter,
            IStageSelectPresenter stageSelectPresenter,
            ISelectedStageRepository selectedStageRepository,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.None, stateEntity)
        {
            PlayerStageSelectionPresenter = playerStageSelectionPresenter;
            StageSelectPresenter = stageSelectPresenter;
            SelectedStageRepository = selectedStageRepository;
        }

        public override void OnEnter()
        {
            PlayerStageSelectionPresenter.SelectEvent += OnSelect;
        }

        public override void OnExit()
        {
            PlayerStageSelectionPresenter.SelectEvent -= OnSelect;
        }

        private void OnSelect(Option<string> selectedStage)
        {
            if (!selectedStage.TryGetValue(out var stage))
            {
                return;
            }
            
            SelectedStageRepository.SetSelectedStage(stage);
            StageSelectPresenter.PresentSelectedStage(stage);
            
            StateEntity.ChangeState(StageSelectStateType.Some);
        }
        
        private IPlayerStageSelectionPresenter PlayerStageSelectionPresenter { get; }
        private IStageSelectPresenter StageSelectPresenter { get; }
        private ISelectedStageRepository SelectedStageRepository { get; }
    }
}