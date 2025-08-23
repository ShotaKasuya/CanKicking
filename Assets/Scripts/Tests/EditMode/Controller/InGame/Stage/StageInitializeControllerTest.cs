using Controller.InGame.Stage;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Module.Option.Runtime;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Stage
{
    public class StageInitializeControllerTest
    {
        // Mocks
        private class MockLazyBaseHeightView : ILazyBaseHeightView
        {
            public OnceCell<float> BaseHeight { get; } = new();
        }

        private class MockBaseHeightView : IBaseHeightView
        {
            public float PositionY => 10f;
        }

        private class MockLazyStartPositionView : ILazyStartPositionView
        {
            public OnceCell<ISpawnPositionView> StartPosition { get; } = new();
        }

        private class MockSpawnPositionView : ISpawnPositionView
        {
            public Transform StartPosition => new GameObject().transform;
        }

        private class MockLazyGoalHeightView : ILazyGoalHeightView
        {
            public OnceCell<float> GoalHeight { get; } = new();
        }

        private class MockGoalHeightView : IGoalHeightView
        {
            public float PositionY => 100f;
        }

        private class MockGoalEventView : IGoalEventView
        {
            private readonly Subject<Unit> _subject = new();
            public Observable<Unit> Performed => _subject;
            public void SimulateGoal() => _subject.OnNext(Unit.Default);
        }

        private class MockGoalEventSubjectModel : IGoalEventSubjectModel
        {
            public Subject<Unit> GoalEventSubject { get; } = new();
        }

        private StageInitializeController _controller;
        private MockLazyBaseHeightView _lazyBaseHeightView;
        private MockBaseHeightView _baseHeightView;
        private MockLazyStartPositionView _lazyStartPositionView;
        private MockSpawnPositionView _spawnPositionView;
        private MockLazyGoalHeightView _lazyGoalHeightView;
        private MockGoalHeightView _goalHeightView;
        private MockGoalEventView _goalEventView;
        private MockGoalEventSubjectModel _goalEventSubjectModel;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _lazyBaseHeightView = new MockLazyBaseHeightView();
            _baseHeightView = new MockBaseHeightView();
            _lazyStartPositionView = new MockLazyStartPositionView();
            _spawnPositionView = new MockSpawnPositionView();
            _lazyGoalHeightView = new MockLazyGoalHeightView();
            _goalHeightView = new MockGoalHeightView();
            _goalEventView = new MockGoalEventView();
            _goalEventSubjectModel = new MockGoalEventSubjectModel();
            _compositeDisposable = new CompositeDisposable();

            _controller = new StageInitializeController(
                _lazyBaseHeightView, _baseHeightView, _lazyStartPositionView, _spawnPositionView,
                _lazyGoalHeightView, _goalHeightView, _goalEventView, _goalEventSubjectModel, _compositeDisposable
            );
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void Initialize_InitializesAllLazyViews()
        {
            // Act
            _controller.Start();

            // Assert
            Assert.IsTrue(_lazyGoalHeightView.GoalHeight.IsInitialized);
            Assert.AreEqual(_goalHeightView.PositionY, _lazyGoalHeightView.GoalHeight.Unwrap());

            Assert.IsTrue(_lazyBaseHeightView.BaseHeight.IsInitialized);
            Assert.AreEqual(_baseHeightView.PositionY, _lazyBaseHeightView.BaseHeight.Unwrap());

            Assert.IsTrue(_lazyStartPositionView.StartPosition.IsInitialized);
            Assert.AreEqual(_spawnPositionView, _lazyStartPositionView.StartPosition.Unwrap());
        }

        [Test]
        public void Initialize_SubscribesToGoalEvent()
        {
            // Arrange
            bool wasGoalEventFired = false;
            _goalEventSubjectModel.GoalEventSubject.Subscribe(_ => wasGoalEventFired = true);

            // Act
            _controller.Start();
            _goalEventView.SimulateGoal();

            // Assert
            Assert.IsTrue(wasGoalEventFired);
        }
    }
}