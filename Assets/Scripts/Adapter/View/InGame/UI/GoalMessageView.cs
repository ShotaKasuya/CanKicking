using Adapter.IView.InGame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Detail.View.InGame.UI
{
    public class GoalMessageView: MonoBehaviour, IGoalMessageView
    {
        [SerializeField] private Text goalMessage;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void ShowGoal(ShowGoalArg arg)
        {
            gameObject.SetActive(true);
            goalMessage.text = "Goal";
        }
    }
}