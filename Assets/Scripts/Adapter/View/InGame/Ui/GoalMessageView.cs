using Adapter.IView.InGame.Ui;
using UnityEngine;

namespace Adapter.View.InGame.Ui
{
    public class GoalMessageView: MonoBehaviour, IGoalMessageView
    {
        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void ShowGoal(ShowGoalArg arg)
        {
            gameObject.SetActive(true);
        }
    }
}