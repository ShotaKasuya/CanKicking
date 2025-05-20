using Adapter.IView.InGame.UI;
using UnityEngine;

namespace Adapter.View.InGame.UI
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