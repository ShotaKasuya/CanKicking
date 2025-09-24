using Controller.InGame.Stage;
using NUnit.Framework;
using R3;
using Tests.Mock.InGame;

namespace Tests.EditMode.Controller.InGame.Stage
{
    public class StageInitializeControllerTest
    {
        private StageInitializeController _controller;
        private MockLazyBaseHeightView _lazyBaseHeightView;
        private MockBaseHeightView _baseHeightView;
        private MockLazyStartPositionView _lazyStartPositionView;
        private MockSpawnPositionView _spawnPositionView;
        private MockLazyGoalHeightView _lazyGoalHeightView;
        private MockGoalHeightView _goalHeightView;
        private MockGoalEventView _goalEventView;
        private MockGoalEventSubjectModel _goalEventSubjectModel;
        private MockStoreClearDataLogic _storeClearDataLogic;
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
            _storeClearDataLogic = new MockStoreClearDataLogic();
            _compositeDisposable = new CompositeDisposable();

            _controller = new StageInitializeController(
                _lazyBaseHeightView,
                _baseHeightView,
                _lazyStartPositionView,
                _spawnPositionView,
                _lazyGoalHeightView,
                _goalHeightView,
                _goalEventView,
                _goalEventSubjectModel,
                _storeClearDataLogic,
                _compositeDisposable
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

        [Test]
        public void OnGoal_CallsSetClearData()
        {
            // Act
            _controller.Start();
            _goalEventView.SimulateGoal();

            // Assert
            Assert.IsTrue(_storeClearDataLogic.IsSetClearDataCalled);
        }
    }
}
