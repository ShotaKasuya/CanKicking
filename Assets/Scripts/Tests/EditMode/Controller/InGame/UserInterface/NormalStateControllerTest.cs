using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Interface.Model.InGame;
using Interface.View.InGame;
using Interface.View.InGame.UserInterface;
using Module.Option.Runtime;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.InGame.UserInterface;
using Tests.EditMode.Mocks;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class NormalStateControllerTest
    {
        // Mocks
        private class MockNormalUiView : INormalUiView
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

        private class MockLazyPlayerView : ILazyPlayerView
        {
            public OnceCell<IPlayerView> PlayerView { get; } = new();
        }

        private class MockLazyBaseHeightView : ILazyBaseHeightView
        {
            public OnceCell<float> BaseHeight { get; } = new();
        }

        private class MockLazyGoalHeightView : ILazyGoalHeightView
        {
            public OnceCell<float> GoalHeight { get; } = new();
        }

        private class MockStopButtonView : IStopButtonView
        {
            private readonly Subject<Unit> _subject = new();
            public Observable<Unit> Performed => _subject;
            public void SimulateClick() => _subject.OnNext(Unit.Default);
        }

        private class MockProgressUiView : IProgressUiView
        {
            public float? Progress { get; private set; }
            public void SetProgress(float progress) => Progress = progress;
        }

        private class MockKickCountUiView : IKickCountUiView
        {
            public int? Count { get; private set; }
            public void SetCount(int count) => Count = count;
        }

        private class MockGoalEventModel : IGoalEventModel
        {
            private readonly Subject<Unit> _subject = new();
            public Observable<Unit> GoalEvent => _subject;
            public void SimulateGoal() => _subject.OnNext(Unit.Default);
        }

        private class MockUiStateEntity : IMutStateEntity<UserInterfaceStateType>
        {
            public UserInterfaceStateType CurrentState { get; private set; }
            public UserInterfaceStateType EntryState { get; }
            public Observable<UserInterfaceStateType> StateExitObservable => _stateExitSubject;
            public Observable<UserInterfaceStateType> StateEnterObservable => _stateEnterSubject;

            private readonly Subject<UserInterfaceStateType> _stateExitSubject = new();
            private readonly Subject<UserInterfaceStateType> _stateEnterSubject = new();
            private readonly OperationPool _operationPool = new OperationPool();

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
                return _operationPool.SpawnOperation(context);
            }
        }

        private NormalStateController _controller;
        private MockNormalUiView _normalUiView;
        private MockLazyPlayerView _lazyPlayerView;
        private MockLazyBaseHeightView _lazyBaseHeightView;
        private MockLazyGoalHeightView _lazyGoalHeightView;
        private MockStopButtonView _stopButtonView;
        private MockProgressUiView _progressUiView;
        private MockKickCountUiView _kickCountUiView;
        private MockGoalEventModel _goalEventModel;
        private MockKickCountModel _kickCountModel;
        private MockUiStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _normalUiView = new MockNormalUiView();
            _lazyPlayerView = new MockLazyPlayerView();
            _lazyBaseHeightView = new MockLazyBaseHeightView();
            _lazyGoalHeightView = new MockLazyGoalHeightView();
            _stopButtonView = new MockStopButtonView();
            _progressUiView = new MockProgressUiView();
            _kickCountUiView = new MockKickCountUiView();
            _goalEventModel = new MockGoalEventModel();
            _kickCountModel = new MockKickCountModel();
            _stateEntity = new MockUiStateEntity(UserInterfaceStateType.Normal);
            _compositeDisposable = new CompositeDisposable();

            _controller = new NormalStateController(
                _normalUiView, _lazyPlayerView, _lazyBaseHeightView, _lazyGoalHeightView,
                _stopButtonView, _progressUiView, _kickCountUiView, _goalEventModel,
                _kickCountModel, _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public async Task OnEnter_ShowsNormalUi()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.IsTrue(_normalUiView.IsShown);
        }

        [Test]
        public async Task OnExit_HidesNormalUi()
        {
            await _controller.OnExit(CancellationToken.None);
            Assert.IsFalse(_normalUiView.IsShown);
        }

        [Test]
        public void OnStopButtonClick_ChangesStateToStop()
        {
            _stopButtonView.SimulateClick();
            Assert.AreEqual(UserInterfaceStateType.Stop, _stateEntity.CurrentState);
        }

        [Test]
        public void OnGoalEvent_ChangesStateToGoal()
        {
            _goalEventModel.SimulateGoal();
            Assert.AreEqual(UserInterfaceStateType.Goal, _stateEntity.CurrentState);
        }

        [Test]
        public void OnJumpCountChanged_UpdatesUi()
        {
            _kickCountModel.Inc();
            Assert.AreEqual(1, _kickCountUiView.Count);
        }

        [Test]
        public void StateUpdate_SetsCorrectProgress()
        {
            var playerView = new MockPlayerView();
            playerView.ModelTransform.position = new Vector3(0, 50, 0);
            _lazyPlayerView.PlayerView.Init(playerView);
            _lazyBaseHeightView.BaseHeight.Init(0f);
            _lazyGoalHeightView.GoalHeight.Init(100f);

            _controller.StateUpdate(0.1f);

            Assert.AreEqual(0.5f, _progressUiView.Progress);
        }
    }
}