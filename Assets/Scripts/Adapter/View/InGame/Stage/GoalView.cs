using Adapter.IView.InGame.Stage;
using Adapter.IView.InGame.Ui;
using R3;
using UnityEngine;

namespace Adapter.View.InGame.Stage
{
    public class GoalView: MonoBehaviour, IPlayerEnterEventView, IGoalEventView
    {
        private void Awake()
        {
            _subject = new Subject<Unit>();
        }

        public void OnPlayerEnter()
        {
            _subject.OnNext(Unit.Default);
        }

        private Subject<Unit> _subject;
        public Observable<Unit> OnPerformed => _subject;
    }
}