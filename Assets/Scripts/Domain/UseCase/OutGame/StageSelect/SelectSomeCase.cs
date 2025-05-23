using Domain.IPresenter.OutGame.StageSelect;
using Domain.IPresenter.Scene;
using Domain.IRepository.OutGame;
using Module.Option;
using Module.StateMachine;
using Structure.OutGame;

namespace Domain.UseCase.OutGame.StageSelect
{
    public class SelectSomeCase : StageSelectStateBehaviourBase
    {
        public SelectSomeCase
        (
            IScenePresenter scenePresenter,
            IPlayerStageSelectionPresenter playerStageSelectionPresenter,
            IStageSelectPresenter stageSelectPresenter,
            ISelectedStageRepository selectedStageRepository,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.Some, stateEntity)
        {
            ScenePresenter = scenePresenter;
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
            var prevSelect = SelectedStageRepository.SelectedStage;
            if (!selectedStage.TryGetValue(out var stage))
            {
                StageSelectPresenter.PresentCancelSelection();
                StateEntity.ChangeState(StageSelectStateType.None);
                return;
            }

            if (stage != prevSelect)
            {
                StageSelectPresenter.PresentCancelSelection();
                SelectedStageRepository.SetSelectedStage(stage);
                return;
            }

            ScenePresenter.Load(prevSelect);
        }

        private IPlayerStageSelectionPresenter PlayerStageSelectionPresenter { get; }
        private IStageSelectPresenter StageSelectPresenter { get; }
        private IScenePresenter ScenePresenter { get; }
        private ISelectedStageRepository SelectedStageRepository { get; }
    }
}