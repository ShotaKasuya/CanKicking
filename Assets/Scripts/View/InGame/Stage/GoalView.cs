using Interface.InGame.Stage;
using R3;
using R3.Triggers;
using UnityEngine;

namespace View.InGame.Stage
{
    public class GoalView : MonoBehaviour, IGoalEventView
    {
        public Observable<Unit> Performed => this.OnTriggerEnter2DAsObservable().AsUnitObservable();
    }
}