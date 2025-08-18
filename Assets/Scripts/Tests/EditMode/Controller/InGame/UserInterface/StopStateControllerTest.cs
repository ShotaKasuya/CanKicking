using System;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Interface.InGame.Primary;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class StopStateControllerTest
    {
        // Mocks
        private class MockStopUiView : IStopUiView
        {
            public bool IsShown { get; private set; }

            public UniTask Show()
            {
                IsShown = true;
                return UniTask.CompletedTask;
            }

            public UniTask Hide()
            {
                IsShown = false;
                return UniTask.CompletedTask;
            }
        }

        private class MockPlayButtonView : IPlayButtonView
        {
            private readonly Subject<Unit> _subject = new();
            public Observable<Unit> Performed => _subject;
            public void SimulateClick() => _subject.OnNext(Unit.Default);
        }

        private class MockStageSelectButtonView : IStop_StageSelectButtonView
        {
            private readonly Subject<string> _subject = new();
            public Observable<string> Performed => _subject;
            public void SimulateClick(string scene) => _subject.OnNext(scene);
        }

        private class MockRestartButtonView : IStop_RestartButtonView
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

        private class MockTimeScaleModel : ITimeScaleModel
        {
            public TimeCommandType? ExecutedCommand { get; private set; }
            public bool IsUndoCalled { get; private set; }
            public void Execute(TimeCommandType timeCommand) => ExecutedCommand = timeCommand;
            public void Undo() => IsUndoCalled = true;

            public void Reset()
            {
            }
        }

        private class MockGameRestartLogic : IGameRestartLogic
        {
            public bool IsRestarted { get; private set; }
            public void RestartGame() => IsRestarted = true;
        }

        private class MockPlayerStateEntity : IMutStateEntity<PlayerStateType>
        {
            public PlayerStateType State { get; private set; }
            public Action<StatePair<PlayerStateType>> OnChangeState { get; set; }
            public bool IsInState(PlayerStateType state) => State == state;
            public void ChangeState(PlayerStateType next) => State = next;
        }

        private class MockUiStateEntity : IMutStateEntity<UserInterfaceStateType>
        {
            public UserInterfaceStateType State { get; private set; } = UserInterfaceStateType.Stop;
            public Action<StatePair<UserInterfaceStateType>> OnChangeState { get; set; }
            public bool IsInState(UserInterfaceStateType state) => State == state;
            public void ChangeState(UserInterfaceStateType next) => State = next;
        }

        private StopStateController _controller;
        private MockStopUiView _stopUiView;
        private MockPlayButtonView _playButtonView;
        private MockStageSelectButtonView _stageSelectButtonView;
        private MockRestartButtonView _restartButtonView;
        private MockLoadPrimarySceneLogic _loadPrimarySceneLogic;
        private MockTimeScaleModel _timeScaleModel;
        private MockGameRestartLogic _gameRestartLogic;
        private MockPlayerStateEntity _playerStateEntity;
        private MockUiStateEntity _uiStateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _stopUiView = new MockStopUiView();
            _playButtonView = new MockPlayButtonView();
            _stageSelectButtonView = new MockStageSelectButtonView();
            _restartButtonView = new MockRestartButtonView();
            _loadPrimarySceneLogic = new MockLoadPrimarySceneLogic();
            _timeScaleModel = new MockTimeScaleModel();
            _gameRestartLogic = new MockGameRestartLogic();
            _playerStateEntity = new MockPlayerStateEntity();
            _uiStateEntity = new MockUiStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new StopStateController(
                _stopUiView, _playButtonView, _stageSelectButtonView, _restartButtonView,
                _loadPrimarySceneLogic, _timeScaleModel, _gameRestartLogic, _compositeDisposable,
                _playerStateEntity, _uiStateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void OnEnter_SetsPlayerState_SetsTimeScale_ShowsUi()
        {
            _controller.OnEnter();
            Assert.AreEqual(PlayerStateType.Stopping, _playerStateEntity.State);
            Assert.AreEqual(TimeCommandType.Stop, _timeScaleModel.ExecutedCommand);
            Assert.IsTrue(_stopUiView.IsShown);
        }

        [Test]
        public void OnExit_ResetsPlayerState_UndoesTimeScale_HidesUi()
        {
            _controller.OnExit();
            Assert.AreEqual(PlayerStateType.Idle, _playerStateEntity.State);
            Assert.IsTrue(_timeScaleModel.IsUndoCalled);
            Assert.IsFalse(_stopUiView.IsShown);
        }

        [Test]
        public void OnPlayButtonClick_ChangesStateToNormal()
        {
            _playButtonView.SimulateClick();
            Assert.AreEqual(UserInterfaceStateType.Normal, _uiStateEntity.State);
        }

        [Test]
        public void OnRestartButtonClick_RestartsGame()
        {
            _restartButtonView.SimulateClick("any");
            Assert.IsTrue(_gameRestartLogic.IsRestarted);
        }

        [Test]
        public void OnStageSelectButtonClick_ChangesScene()
        {
            var scene = "NewScene";
            _stageSelectButtonView.SimulateClick(scene);
            Assert.AreEqual(scene, _loadPrimarySceneLogic.CalledScenePath);
        }
    }
}