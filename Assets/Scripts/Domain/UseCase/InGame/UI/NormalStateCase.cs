using Domain.IPresenter.InGame.Stage;
using Domain.IPresenter.InGame.UI;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.UserInterface;

namespace Domain.UseCase.InGame.UI
{
    public class NormalStateCase : UserInterfaceBehaviourBase
    {
        public NormalStateCase
        (
            IHeightUiPresenter heightUiPresenter,
            IPlayerHeightPresenter playerHeightPresenter,
            IGoalEventPresenter goalEventPresenter,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Normal, stateEntity)
        {
            GoalEventPresenter = goalEventPresenter;
            HeightUiPresenter = heightUiPresenter;
            PlayerHeightPresenter = playerHeightPresenter;
        }

        public override void OnEnter()
        {
            GoalEventPresenter.GoalEvent += OnGoal;
        }

        public override void StateUpdate(float deltaTime)
        {
            float height = PlayerHeightPresenter.GetHeight();
            HeightUiPresenter.SetHeight(height);
        }

        public override void OnExit()
        {
            GoalEventPresenter.GoalEvent -= OnGoal;
        }

        private void OnGoal()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Goal);
        }

        private IGoalEventPresenter GoalEventPresenter { get; }
        private IHeightUiPresenter HeightUiPresenter { get; }
        private IPlayerHeightPresenter PlayerHeightPresenter { get; }
    }
}