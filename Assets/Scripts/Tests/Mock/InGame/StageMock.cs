using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Interface.Model.InGame;
using Interface.View.InGame;
using Module.Option.Runtime;
using R3;
using UnityEngine;

namespace Tests.Mock.InGame
{
    public class MockGoalEventModel : IGoalEventModel
    {
        private readonly Subject<Unit> _subject = new();
        public Observable<Unit> GoalEvent => _subject;
        public void SimulateGoal() => _subject.OnNext(Unit.Default);
    }

    // Mocks moved from StageInitializeControllerTest
    public class MockBaseHeightView : IBaseHeightView
    {
        public float PositionY => 10f;
    }

    public class MockLazyStartPositionView : ILazyStartPositionView
    {
        public OnceCell<ISpawnPositionView> StartPosition { get; } = new();
    }

    public class MockSpawnPositionView : ISpawnPositionView
    {
        public Transform StartPosition => new GameObject().transform;
    }

    public class MockGoalHeightView : IGoalHeightView
    {
        public float PositionY => 100f;
    }

    public class MockGoalEventView : IGoalEventView
    {
        private readonly Subject<Unit> _subject = new();
        public Observable<Unit> Performed => _subject;
        public void SimulateGoal() => _subject.OnNext(Unit.Default);
    }

    public class MockGoalEventSubjectModel : IGoalEventSubjectModel
    {
        public Subject<Unit> GoalEventSubject { get; } = new();
    }

    public class MockStoreClearDataLogic : IStoreClearDataLogic
    {
        public bool IsSetClearDataCalled { get; private set; }
        public UniTask StoreClearData()
        {
            IsSetClearDataCalled = true;
            return UniTask.CompletedTask;
        }
    }
}