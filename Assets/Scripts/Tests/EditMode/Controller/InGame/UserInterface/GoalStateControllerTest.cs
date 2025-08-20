using System;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.InGame.Primary;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Module.Option.Runtime;
using Structure.InGame.UserInterface;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class GoalStateControllerTest
    {
        // Mocks
        private class MockGoalUiView : IGoalUiView
        {
            public bool IsShown { get; private set; }

            public UniTask Show()
            {
                IsShown = true;
                return UniTask.CompletedTask;
            }
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
            public UserInterfaceStateType CurrentState { get; private set; }
            public UserInterfaceStateType EntryState { get; }
            public Observable<UserInterfaceStateType> StateExitObservable => _stateExitSubject;
            public Observable<UserInterfaceStateType> StateEnterObservable => _stateEnterSubject;

            private readonly Subject<UserInterfaceStateType> _stateExitSubject = new();
            private readonly Subject<UserInterfaceStateType> _stateEnterSubject = new();

            public MockUiStateEntity(UserInterfaceStateType initialState)
            {
                CurrentState = initialState;
                EntryState = initialState;
            }

            public bool IsInState(UserInterfaceStateType state)
            {
                return CurrentState == state;
            }

            public UniTask ChangeState(UserInterfaceStateType next)
            {
                _stateExitSubject.OnNext(CurrentState);
                CurrentState = next;
                _stateEnterSubject.OnNext(next);
                return UniTask.CompletedTask;
            }

            public OperationHandle GetStateLock(string context)
            {
                throw new NotImplementedException();
            }
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
            _uiStateEntity = new MockUiStateEntity(UserInterfaceStateType.Goal);
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