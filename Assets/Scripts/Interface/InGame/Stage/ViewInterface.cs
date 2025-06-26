using R3;

namespace Interface.InGame.Stage
{
    public interface IGoalEventView
    {
        public Observable<Unit> Performed { get; }
    }
}