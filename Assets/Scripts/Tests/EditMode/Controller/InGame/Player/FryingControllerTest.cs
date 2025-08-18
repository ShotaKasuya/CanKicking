using System;
using Controller.InGame.Player;
using Interface.Global.TimeScale;
using Interface.InGame.Player;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.Utility;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class FryingControllerTest
    {
        // Mocks
        private class MockPlayerView : IPlayerView
        {
            private readonly Subject<Collision2D> _collisionSubject = new();
            public Transform ModelTransform { get; } = new GameObject().transform;
            public Vector2 LinearVelocity { get; set; } = Vector2.down;
            public float AngularVelocity => 0f;
            public Observable<Collision2D> CollisionEnterEvent => _collisionSubject;

            public void Activation(bool isActive)
            {
            }

            public void ResetPosition(Vector2 position)
            {
            }

            public void SimulateCollision(Collision2D collision) => _collisionSubject.OnNext(collision);
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

        private class MockTimeScaleModel : ITimeScaleModel
        {
            public TimeCommandType? ExecutedCommand { get; private set; }
            public bool IsUndoCalled { get; private set; }
            public bool IsResetCalled { get; private set; }
            public void Execute(TimeCommandType timeCommand) => ExecutedCommand = timeCommand;
            public void Undo() => IsUndoCalled = true;
            public void Reset() => IsResetCalled = true;
        }

        private class MockStateEntity : IMutStateEntity<PlayerStateType>
        {
            public PlayerStateType State { get; private set; } = PlayerStateType.Frying;
            public Action<StatePair<PlayerStateType>> OnChangeState { get; set; }
            public bool IsInState(PlayerStateType state) => State == state;

            public void ChangeState(PlayerStateType next)
            {
                State = next;
            }
        }

        private FryingController _controller;
        private MockPlayerView _playerView;
        private MockRayCasterView _rayCasterView;
        private MockGroundDetectionModel _groundDetectionModel;
        private MockTimeScaleModel _timeScaleModel;
        private MockStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _playerView = new MockPlayerView();
            _rayCasterView = new MockRayCasterView();
            _groundDetectionModel = new MockGroundDetectionModel();
            _timeScaleModel = new MockTimeScaleModel();
            _stateEntity = new MockStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new FryingController(
                _playerView,
                _rayCasterView,
                _groundDetectionModel,
                _timeScaleModel,
                _compositeDisposable,
                _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void OnEnter_SetsFryingTimeScale()
        {
            _controller.OnEnter();
            Assert.AreEqual(TimeCommandType.Frying, _timeScaleModel.ExecutedCommand);
        }

        [Test]
        public void OnExit_UndoesTimeScale()
        {
            _controller.OnExit();
            Assert.IsTrue(_timeScaleModel.IsUndoCalled);
        }

        [Test]
        public void StateUpdate_WhenGrounded_ChangesStateToIdle()
        {
            var hit = new RaycastHit2D { normal = Vector2.up };
            _rayCasterView.HitsToReturn = new[] { hit };

            _controller.StateUpdate(0.1f);

            Assert.AreEqual(PlayerStateType.Idle, _stateEntity.State);
        }
    }
}