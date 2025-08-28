using System;
using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Logic.InGame;
using Interface.Model.Global;
using Interface.View.InGame.UserInterface;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Module.Option.Runtime;
using Structure.InGame.UserInterface;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class StopStateControllerTest
    {
        // Mocks
        private class MockStopUiView : IStopUiView
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
            public PlayerStateType CurrentState { get; private set; }
            public PlayerStateType EntryState { get; }
            public Observable<PlayerStateType> StateExitObservable => _stateExitSubject;
            public Observable<PlayerStateType> StateEnterObservable => _stateEnterSubject;

            private readonly Subject<PlayerStateType> _stateExitSubject = new();
            private readonly Subject<PlayerStateType> _stateEnterSubject = new();

            public MockPlayerStateEntity(PlayerStateType initialState)
            {
                CurrentState = initialState;
                EntryState = initialState;
            }

            public bool IsInState(PlayerStateType state)
            {
                return CurrentState == state;
            }

            public UniTask ChangeState(PlayerStateType next)
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
            _playerStateEntity = new MockPlayerStateEntity(default);
            _uiStateEntity = new MockUiStateEntity(UserInterfaceStateType.Stop);
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
        public async Task OnEnter_SetsPlayerState_SetsTimeScale_ShowsUi()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.AreEqual(PlayerStateType.Stopping, _playerStateEntity.CurrentState);
            Assert.AreEqual(TimeCommandType.Stop, _timeScaleModel.ExecutedCommand);
            Assert.IsTrue(_stopUiView.IsShown);
        }

        [Test]
        public async Task OnExit_ResetsPlayerState_UndoesTimeScale_HidesUi()
        {
            await _controller.OnExit(CancellationToken.None);
            Assert.AreEqual(PlayerStateType.Idle, _playerStateEntity.CurrentState);
            Assert.IsTrue(_timeScaleModel.IsUndoCalled);
            Assert.IsFalse(_stopUiView.IsShown);
        }

        [Test]
        public void OnPlayButtonClick_ChangesStateToNormal()
        {
            _playButtonView.SimulateClick();
            Assert.AreEqual(UserInterfaceStateType.Normal, _uiStateEntity.CurrentState);
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