using System;
using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.Player;
using Cysharp.Threading.Tasks;
using Interface.Model.Global;
using Interface.Model.InGame;
using Interface.View.InGame;
using Module.Option.Runtime;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.Utility;
using Tests.EditMode.Mocks;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class FryingControllerTest
    {
        // Mocks

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

        private FryingController _controller;
        private MockPlayerView _playerView;
        private MockRayCasterView _rayCasterView;
        private MockGroundDetectionModel _groundDetectionModel;
        private MockTimeScaleModel _timeScaleModel;
        private MockStateEntity<PlayerStateType> _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _playerView = new MockPlayerView();
            _rayCasterView = new MockRayCasterView();
            _groundDetectionModel = new MockGroundDetectionModel();
            _timeScaleModel = new MockTimeScaleModel();
            _stateEntity = new MockStateEntity<PlayerStateType>(PlayerStateType.Frying);
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
        public async Task OnEnter_SetsFryingTimeScale()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.AreEqual(TimeCommandType.Frying, _timeScaleModel.ExecutedCommand);
        }

        [Test]
        public async Task OnExit_UndoesTimeScale()
        {
            await _controller.OnExit(CancellationToken.None);
            Assert.IsTrue(_timeScaleModel.IsUndoCalled);
        }

        [Test]
        public void StateUpdate_WhenGrounded_ChangesStateToIdle()
        {
            var hit = new RaycastHit2D { normal = Vector2.up };
            _rayCasterView.HitsToReturn = new[] { hit };

            _controller.StateUpdate(0.1f);

            Assert.AreEqual(PlayerStateType.Idle, _stateEntity.CurrentState);
        }
    }
}