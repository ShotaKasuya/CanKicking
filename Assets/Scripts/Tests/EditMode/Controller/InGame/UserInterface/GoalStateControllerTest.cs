using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.Player;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Logic.InGame;
using Interface.View.InGame.UserInterface;
using NUnit.Framework;
using R3;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class GoalStateControllerTest
    {
        // Mocks
        private class MockGoalUiView : IGoalUiView
        {
            public bool IsShown { get; private set; }

            public UniTask Show(CancellationToken token)
            {
                IsShown = true;
                return UniTask.CompletedTask;
            }

            public UniTask Hide(CancellationToken token)
            {
                IsShown = false;
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

        private GoalStateController _controller;
        private MockGoalUiView _goalUiView;
        private MockRestartButtonView _restartButtonView;
        private MockStageSelectButtonView _stageSelectButtonView;
        private MockLoadPrimarySceneLogic _loadPrimarySceneLogic;
        private MockGameRestartLogic _gameRestartLogic;
        private PlayerState _playerState;
        private UserInterfaceState _uiStateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public async Task SetUp()
        {
            _goalUiView = new MockGoalUiView();
            _restartButtonView = new MockRestartButtonView();
            _stageSelectButtonView = new MockStageSelectButtonView();
            _loadPrimarySceneLogic = new MockLoadPrimarySceneLogic();
            _gameRestartLogic = new MockGameRestartLogic();
            _playerState = new PlayerState();
            _uiStateEntity = new UserInterfaceState();
            await _uiStateEntity.ChangeState(UserInterfaceStateType.Goal);
            _compositeDisposable = new CompositeDisposable();

            _controller = new GoalStateController(
                _goalUiView, _restartButtonView, _stageSelectButtonView,
                _loadPrimarySceneLogic, _gameRestartLogic, _compositeDisposable,
                _playerState, _uiStateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public async Task OnEnter_ShowsGoalUi_AndChangesPlayerState()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.IsTrue(_goalUiView.IsShown);
            Assert.AreEqual(PlayerStateType.Stopping, _playerState.CurrentState);
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