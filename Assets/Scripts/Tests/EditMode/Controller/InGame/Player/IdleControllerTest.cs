
using System;
using Controller.InGame.Player;
using Interface.Global.Input;
using Interface.Global.Utility;
using Interface.InGame.Player;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.InGame.Player;
using Structure.Utility;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class IdleControllerTest
    {
        // Mocks
        private class MockTouchView : ITouchView
        {
            public Observable<TouchStartEventArgument> TouchEvent => Observable.Empty<TouchStartEventArgument>();
            public Module.Option.Runtime.Option<FingerDraggingInfo> DraggingInfo { get; set; } = Module.Option.Runtime.Option<FingerDraggingInfo>.None();
            public Observable<TouchEndEventArgument> TouchEndEvent => Observable.Empty<TouchEndEventArgument>();
        }

        private class MockDoubleTapView : IDoubleTapView
        {
            private readonly Subject<Unit> _doubleTapSubject = new();
            public Observable<Unit> DoubleTapEvent => _doubleTapSubject;
            public void SimulateDoubleTap() => _doubleTapSubject.OnNext(Unit.Default);
        }

        private class MockPlayerView : IPlayerView
        {
            public Transform ModelTransform { get; } = new GameObject().transform;
            public Vector2 LinearVelocity => Vector2.zero;
            public float AngularVelocity => 0f;
            public Observable<Collision2D> CollisionEnterEvent => Observable.Empty<Collision2D>();
            public Vector2? ResetPositionValue { get; private set; }

            public void Activation(bool isActive) { }
            public void ResetPosition(Vector2 position) { ResetPositionValue = position; }
        }

        private class MockRayCasterView : IRayCasterView
        {
            public RaycastHit2D[] HitsToReturn = Array.Empty<RaycastHit2D>();
            public ReadOnlySpan<RaycastHit2D> PoolRay(RayCastInfo rayCastInfo) => new(HitsToReturn);
        }

        private class MockGroundDetectionModel : IGroundDetectionModel
        {
            public RayCastInfo GroundDetectionInfo => new(Vector2.down, 1f, 1);
            public float MaxSlope { get; set; } = 45f;
        }

        private class MockPullLimitModel : IPullLimitModel
        {
            public float CancelRatio { get; set; } = 0.1f;
            public float MaxRatio { get; set; } = 1.0f;
        }

        private class MockScreenScaleModel : IScreenScaleModel
        {
            public Vector2 Scale { get; set; } = new(1080, 1920);
            public float Width => Scale.x;
            public float Height => Scale.y;
        }

        private class MockKickPositionModel : IKickPositionModel
        {
            private Vector2? _positionToPop;
            public void SetPositionToPop(Vector2? pos) => _positionToPop = pos;
            public Module.Option.Runtime.Option<Vector2> PopPosition() => _positionToPop.HasValue ? Module.Option.Runtime.Option<Vector2>.Some(_positionToPop.Value) : Module.Option.Runtime.Option<Vector2>.None();
            public void PushPosition(Vector2 position) { }
        }

        private class MockStateEntity : IMutStateEntity<PlayerStateType>
        {
            public PlayerStateType State { get; private set; } = PlayerStateType.Idle;
            public Action<StatePair<PlayerStateType>> OnChangeState { get; set; }
            public bool IsInState(PlayerStateType state) => State == state;
            public void ChangeState(PlayerStateType next) { State = next; }
        }

        private IdleController _controller;
        private MockTouchView _touchView;
        private MockDoubleTapView _doubleTapView;
        private MockPlayerView _playerView;
        private MockRayCasterView _rayCasterView;
        private MockGroundDetectionModel _groundDetectionModel;
        private MockPullLimitModel _pullLimitModel;
        private MockScreenScaleModel _screenScaleModel;
        private MockKickPositionModel _kickPositionModel;
        private MockStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _touchView = new MockTouchView();
            _doubleTapView = new MockDoubleTapView();
            _playerView = new MockPlayerView();
            _rayCasterView = new MockRayCasterView();
            _groundDetectionModel = new MockGroundDetectionModel();
            _pullLimitModel = new MockPullLimitModel();
            _screenScaleModel = new MockScreenScaleModel();
            _kickPositionModel = new MockKickPositionModel();
            _stateEntity = new MockStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new IdleController(
                _touchView,
                _doubleTapView,
                _playerView,
                _rayCasterView,
                _groundDetectionModel,
                _pullLimitModel,
                _screenScaleModel,
                _kickPositionModel,
                _compositeDisposable,
                _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown()
        {
            _compositeDisposable.Dispose();
        }

        [Test]
        public void StateUpdate_NotGrounded_ChangesStateToFrying()
        {
            // Arrange
            _rayCasterView.HitsToReturn = Array.Empty<RaycastHit2D>(); // Not grounded

            // Act
            _controller.StateUpdate(0.1f);

            // Assert
            Assert.AreEqual(PlayerStateType.Frying, _stateEntity.State);
        }

        [Test]
        public void StateUpdate_IsAiming_ChangesStateToAiming()
        {
            // Arrange
            var hit = new RaycastHit2D { normal = Vector2.up };
            _rayCasterView.HitsToReturn = new[] { hit }; // Grounded
            var dragDelta = new Vector2(0, _screenScaleModel.Height * _pullLimitModel.CancelRatio * 1.1f);
            _touchView.DraggingInfo = Module.Option.Runtime.Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(Vector2.zero, dragDelta, Vector2.zero));

            // Act
            _controller.StateUpdate(0.1f);

            // Assert
            Assert.AreEqual(PlayerStateType.Aiming, _stateEntity.State);
        }
        
        [Test]
        public void OnDoubleTap_WithPositionToUndo_ResetsPlayerPosition()
        {
            // Arrange
            var undoPosition = new Vector2(5, 5);
            _kickPositionModel.SetPositionToPop(undoPosition);

            // Act
            _doubleTapView.SimulateDoubleTap();

            // Assert
            Assert.IsTrue(_playerView.ResetPositionValue.HasValue);
            Assert.AreEqual(undoPosition, _playerView.ResetPositionValue.Value);
        }
    }
}
