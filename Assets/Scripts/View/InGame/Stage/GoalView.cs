using Interface.View.InGame;
using R3;
using R3.Triggers;
using UnityEngine;

namespace View.InGame.Stage
{
    public class GoalView : MonoBehaviour, IGoalEventView, IGoalHeightView
    {
        public Observable<Unit> Performed => this.OnTriggerEnter2DAsObservable().AsUnitObservable();
        public float PositionY => transform.position.y;
    }
}