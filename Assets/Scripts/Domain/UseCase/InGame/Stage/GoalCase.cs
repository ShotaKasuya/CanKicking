using System;
using Domain.IPresenter.InGame.Stage;
using Domain.IPresenter.InGame.UI;

namespace Domain.UseCase.InGame.Stage
{
    public class GoalCase: IDisposable
    {
        public GoalCase
        (
            IGoalEventPresenter goalEventPresenter,
            IGoalPresenter goalPresenter
        )
        {
            GoalEventPresenter = goalEventPresenter;
            GoalPresenter = goalPresenter;

            GoalEventPresenter.GoalEvent += OnGoal;
        }

        private void OnGoal()
        {
            GoalPresenter.Goal(new GoalArg());
        }
        
        private IGoalEventPresenter GoalEventPresenter { get; }
        private IGoalPresenter GoalPresenter { get; }

        public void Dispose()
        {
            GoalEventPresenter.GoalEvent -= OnGoal;
        }
    }
}