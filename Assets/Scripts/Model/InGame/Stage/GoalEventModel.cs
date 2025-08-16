using Interface.InGame.Stage;
using R3;

namespace Model.InGame.Stage
{
    public class GoalEventModel : IGoalEventModel, IGoalEventSubjectModel
    {
        public GoalEventModel(CompositeDisposable compositeDisposable)
        {
            GoalEventSubject = new Subject<Unit>().AddTo(compositeDisposable);
        }

        public Observable<Unit> GoalEvent => GoalEventSubject;
        public Subject<Unit> GoalEventSubject { get; }
    }
}