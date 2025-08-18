
using System;
using Controller.InGame.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Interface.InGame.UserInterface;
using Module.Option.Runtime;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.InGame.UserInterface;
using UnityEngine;
using Interface.InGame.Player;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class NormalStateControllerTest
    {
        // Mocks
        private class MockNormalUiView : INormalUiView
        {
            public bool IsShown { get; private set; }
            public UniTask Show() { IsShown = true; return UniTask.CompletedTask; }
            public UniTask Hide() { IsShown = false; return UniTask.CompletedTask; }
        }

        private class MockPlayerView : IPlayerView
        {
            public Transform ModelTransform { get; } = new GameObject().transform;
            public Vector2 LinearVelocity => Vector2.zero; public float AngularVelocity => 0f;
            public Observable<Collision2D> CollisionEnterEvent => Observable.Empty<Collision2D>();
            public void Activation(bool isActive) { } public void ResetPosition(Vector2 position) { }
        }

        private class MockLazyPlayerView : ILazyPlayerView { public OnceCell<IPlayerView> PlayerView { get; } = new(); }
        private class MockLazyBaseHeightView : ILazyBaseHeightView { public OnceCell<float> BaseHeight { get; } = new(); }
        private class MockLazyGoalHeightView : ILazyGoalHeightView { public OnceCell<float> GoalHeight { get; } = new(); }

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

        private class MockJumpCountUiView : IJumpCountUiView
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

        private class MockJumpCountModel : IJumpCountModel, IResetable
        {
            private readonly ReactiveProperty<int> _jumpCount = new(0);
            public ReadOnlyReactiveProperty<int> JumpCount => _jumpCount;
            public void Inc() => _jumpCount.Value++;
            public void Reset() => _jumpCount.Value = 0;
        }

        private class MockUiStateEntity : IMutStateEntity<UserInterfaceStateType>
        {
            public UserInterfaceStateType State { get; private set; } = UserInterfaceStateType.Normal;
            public Action<StatePair<UserInterfaceStateType>> OnChangeState { get; set; }
            public bool IsInState(UserInterfaceStateType state) => State == state;
            public void ChangeState(UserInterfaceStateType next) { State = next; }
        }

        private NormalStateController _controller;
        private MockNormalUiView _normalUiView;
        private MockLazyPlayerView _lazyPlayerView;
        private MockLazyBaseHeightView _lazyBaseHeightView;
        private MockLazyGoalHeightView _lazyGoalHeightView;
        private MockStopButtonView _stopButtonView;
        private MockProgressUiView _progressUiView;
        private MockJumpCountUiView _jumpCountUiView;
        private MockGoalEventModel _goalEventModel;
        private MockJumpCountModel _jumpCountModel;
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
            _jumpCountUiView = new MockJumpCountUiView();
            _goalEventModel = new MockGoalEventModel();
            _jumpCountModel = new MockJumpCountModel();
            _stateEntity = new MockUiStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new NormalStateController(
                _normalUiView, _lazyPlayerView, _lazyBaseHeightView, _lazyGoalHeightView,
                _stopButtonView, _progressUiView, _jumpCountUiView, _goalEventModel,
                _jumpCountModel, _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test] public void OnEnter_ShowsNormalUi() { _controller.OnEnter(); Assert.IsTrue(_normalUiView.IsShown); }
        [Test] public void OnExit_HidesNormalUi() { _controller.OnExit(); Assert.IsFalse(_normalUiView.IsShown); }

        [Test]
        public void OnStopButtonClick_ChangesStateToStop()
        {
            _stopButtonView.SimulateClick();
            Assert.AreEqual(UserInterfaceStateType.Stop, _stateEntity.State);
        }

        [Test]
        public void OnGoalEvent_ChangesStateToGoal()
        {
            _goalEventModel.SimulateGoal();
            Assert.AreEqual(UserInterfaceStateType.Goal, _stateEntity.State);
        }

        [Test]
        public void OnJumpCountChanged_UpdatesUi()
        {
            _jumpCountModel.Inc();
            Assert.AreEqual(1, _jumpCountUiView.Count);
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
