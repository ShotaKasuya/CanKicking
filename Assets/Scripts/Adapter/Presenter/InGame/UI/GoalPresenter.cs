using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class GoalPresenter: IGoalPresenter
    {
        public GoalPresenter
        (
            IGoalMessageView goalMessageView
        )
        {
            GoalMessageView = goalMessageView;
        }
        
        public void Goal(GoalArg arg)
        {
            GoalMessageView.ShowGoal(new ShowGoalArg());
        }
        
        private IGoalMessageView GoalMessageView { get; }
    }
}