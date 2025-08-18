using System;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.InGame.Primary;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.InGame.UserInterface;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class GoalStateControllerTest
    {
        // Mocks
        private class MockGoalUiView : IGoalUiView
        {
            public bool IsShown { get; private set; }
            public void Show() => IsShown = true;
        }

        private class MockRestartButtonView : IGoal_RestartButtonView
        {
            private readonly Subject<string> _subject = new();
            public Observable<string> Performed => _subject;
            public void SimulateClick(string scene) => _subject.OnNext(scene);
        }

        private class MockStageSelectButtonView : IGoal_StageSelectButtonView
        {
            private readonly Subject<string> _subject = new();
            public Observable<string> Performed => _subject;
            public void SimulateClick(string scene) => _subject.OnNext(scene);
        }

        private class MockLoadPrimarySceneLogic : ILoadPrimarySceneLogic
        {
            public string CalledScenePath { get; private set; }

            public UniTask ChangeScene(string scenePath)
            {
                CalledScenePath = scenePath;
                return UniTask.CompletedTask;
            }
        }

        private class MockGameRestartLogic : IGameRestartLogic
        {
            public bool IsRestarted { get; private set; }
            public void RestartGame() => IsRestarted = true;
        }

        private class MockUiStateEntity : IMutStateEntity<UserInterfaceStateType>
        {
            public UserInterfaceStateType State { get; private set; } = UserInterfaceStateType.Goal;
            public Action<StatePair<UserInterfaceStateType>> OnChangeState { get; set; }
            public bool IsInState(UserInterfaceStateType state) => State == state;
            public void ChangeState(UserInterfaceStateType next) => State = next;
        }

        private GoalStateController _controller;
        private MockGoalUiView _goalUiView;
        private MockRestartButtonView _restartButtonView;
        private MockStageSelectButtonView _stageSelectButtonView;
        private MockLoadPrimarySceneLogic _loadPrimarySceneLogic;
        private MockGameRestartLogic _gameRestartLogic;
        private MockUiStateEntity _uiStateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _goalUiView = new MockGoalUiView();
            _restartButtonView = new MockRestartButtonView();
            _stageSelectButtonView = new MockStageSelectButtonView();
            _loadPrimarySceneLogic = new MockLoadPrimarySceneLogic();
            _gameRestartLogic = new MockGameRestartLogic();
            _uiStateEntity = new MockUiStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new GoalStateController(
                _goalUiView, _restartButtonView, _stageSelectButtonView,
                _loadPrimarySceneLogic, _gameRestartLogic, _compositeDisposable, _uiStateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void OnEnter_ShowsGoalUi()
        {
            _controller.OnEnter();
            Assert.IsTrue(_goalUiView.IsShown);
        }

        [Test]
        public void OnRestartButtonClick_RestartsGame()
        {
            _restartButtonView.SimulateClick("any_scene");
            Assert.IsTrue(_gameRestartLogic.IsRestarted);
        }

        [Test]
        public void OnStageSelectButtonClick_ChangesScene()
        {
            const string sceneName = "StageSelectScene";
            _stageSelectButtonView.SimulateClick(sceneName);
            Assert.AreEqual(sceneName, _loadPrimarySceneLogic.CalledScenePath);
        }
    }
}