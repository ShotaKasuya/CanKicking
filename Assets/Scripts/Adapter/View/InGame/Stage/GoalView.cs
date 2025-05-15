using System;
using Adapter.IView.InGame.Stage;
using UnityEngine;

namespace Adapter.View.InGame.Stage
{
    public class GoalView: MonoBehaviour, IPlayerEnterEventView, IGoalView
    {
        public Action TouchGoalEvent { get; set; }
        public void OnPlayerEnter()
        {
            TouchGoalEvent?.Invoke();
        }
    }
}