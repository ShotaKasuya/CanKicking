using Interface.Model.InGame;
using R3;

namespace Tests.Mock.InGame
{
    public class MockGoalEventModel : IGoalEventModel
    {
        private readonly Subject<Unit> _subject = new();
        public Observable<Unit> GoalEvent => _subject;
        public void SimulateGoal() => _subject.OnNext(Unit.Default);
    }
}