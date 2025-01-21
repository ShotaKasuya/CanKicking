using System;
using Adapter.IView.InGame.Stage;
using Domain.IPresenter.InGame.Stage;

namespace Adapter.Presenter.InGame.Stage
{
    public class GoalEventPresenter: IGoalEventPresenter, IDisposable
    {
        public GoalEventPresenter
        (
            IGoalView goalView
        )
        {
            GoalView = goalView;

            goalView.TouchGoalEvent += OnGoal;
        }

        private void OnGoal()
        {
            GoalEvent?.Invoke();
        }
        
        public Action GoalEvent { get; set; }
        
        private IGoalView GoalView { get; }

        public void Dispose()
        {
            GoalView.TouchGoalEvent -= OnGoal;
        }
    }
}