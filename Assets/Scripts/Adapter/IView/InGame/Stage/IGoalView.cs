using System;

namespace Adapter.IView.InGame.Stage
{
    public interface IGoalView
    {
        public Action TouchGoalEvent { get; set; }
    }
}