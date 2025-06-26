using Interface.InGame.Stage;
using R3;
using UnityEngine;

namespace View.InGame.Stage
{
    public class GoalView : MonoBehaviour, IGoalEventView
    {
        public Observable<Unit> Performed => EventSubject;

        private Subject<Unit> EventSubject { get; } = new Subject<Unit>();

        private void OnTriggerEnter2D(Collider2D other)
        {
            EventSubject.OnNext(Unit.Default);
        }
    }
}