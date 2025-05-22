using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class GoalPresenter : IGoalUiPresenter
    {
        public GoalPresenter
        (
            IGoalMessageView goalMessageView
        )
        {
            GoalMessageView = goalMessageView;
        }


        public void ShowUi()
        {
            GoalMessageView.ShowGoal(new ShowGoalArg());
        }

        public void HideUi()
        {
            throw new System.NotImplementedException();
        }

        private IGoalMessageView GoalMessageView { get; }
    }
}