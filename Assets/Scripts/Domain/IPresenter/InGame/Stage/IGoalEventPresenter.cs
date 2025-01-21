using System;

namespace Domain.IPresenter.InGame.Stage
{
    public interface IGoalEventPresenter
    {
        public Action GoalEvent { get; set; }
    }
}