using Domain.IPresenter.InGame.Stage;
using Domain.IPresenter.InGame.UI;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.UserInterface;

namespace Domain.UseCase.InGame.UI
{
    /// <summary>
    /// 通常のUIを管理する
    /// </summary>
    public class NormalStateCase : UserInterfaceBehaviourBase
    {
        public NormalStateCase
        (
            INormalUiPresenter normalUiPresenter,
            IStopEventPresenter stopEventPresenter,
            IHeightUiPresenter heightUiPresenter,
            IPlayerHeightPresenter playerHeightPresenter,
            IGoalEventPresenter goalEventPresenter,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Normal, stateEntity)
        {
            NormalUiPresenter = normalUiPresenter;
            StopEventPresenter = stopEventPresenter;
            GoalEventPresenter = goalEventPresenter;
            HeightUiPresenter = heightUiPresenter;
            PlayerHeightPresenter = playerHeightPresenter;
        }

        public override void OnEnter()
        {
            StopEventPresenter.StopEvent += OnStop;
            GoalEventPresenter.GoalEvent += OnGoal;
            NormalUiPresenter.ShowUi();
        }

        public override void StateUpdate(float deltaTime)
        {
            float height = PlayerHeightPresenter.GetHeight();
            HeightUiPresenter.SetHeight(height);
        }

        public override void OnExit()
        {
            StopEventPresenter.StopEvent -= OnStop;
            GoalEventPresenter.GoalEvent -= OnGoal;
            NormalUiPresenter.HideUi();
        }

        private void OnGoal()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Goal);
        }

        private void OnStop()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Stop);
        }

        private INormalUiPresenter NormalUiPresenter { get; }
        private IStopEventPresenter StopEventPresenter { get; }
        private IGoalEventPresenter GoalEventPresenter { get; }
        private IHeightUiPresenter HeightUiPresenter { get; }
        private IPlayerHeightPresenter PlayerHeightPresenter { get; }
    }
}