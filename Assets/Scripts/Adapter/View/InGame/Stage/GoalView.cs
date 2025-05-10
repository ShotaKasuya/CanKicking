using System;
using Adapter.IView.InGame.Stage;
using UnityEngine;

namespace Adapter.View.InGame.Stage
{
    public class GoalView: MonoBehaviour, IGoalView
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTouchGoal();
        }

        private void OnTouchGoal()
        {
            TouchGoalEvent?.Invoke();
        }
        
        public Action TouchGoalEvent { get; set; }
    }
}