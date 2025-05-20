using Domain.IPresenter.OutGame.StageSelect;
using Domain.IPresenter.Scene;
using Domain.IRepository.OutGame;
using Domain.IUseCase.OutGame;
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
            ISelectedStagePresenter selectedStagePresenter,
            ISelectedStageRepository selectedStageRepository,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.Some, stateEntity)
        {
            ScenePresenter = scenePresenter;
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
            var prevSelect = SelectedStageRepository.SelectedStage;
            if (selectedStage.TryGetValue(out var stage))
            {
                StateEntity.ChangeState(StageSelectStateType.None);
                return;
            }

            if (stage != prevSelect)
            {
                SelectedStageRepository.SetSelectedStage(stage);
                return;
            }

            ScenePresenter.Load(prevSelect);
        }

        private ISelectedStagePresenter SelectedStagePresenter { get; }
        private IScenePresenter ScenePresenter { get; }
        private ISelectedStageRepository SelectedStageRepository { get; }
    }
}