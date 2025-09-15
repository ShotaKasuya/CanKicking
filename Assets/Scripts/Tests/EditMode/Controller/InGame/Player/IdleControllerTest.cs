using System;
using System.Threading.Tasks;
using Controller.InGame.Player;
using Interface.View.Global;
using Module.Option.Runtime;
using NUnit.Framework;
using R3;
using Structure.InGame.Player;
using Tests.Mock;
using Tests.Mock.Global;
using Tests.Mock.InGame;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class IdleControllerTest
    {
        private IdleController _controller;
        private MockTouchView _touchView;
        private MockDoubleTapView _doubleTapView;
        private MockPlayerView _playerView;
        private MockRayCasterView _rayCasterView;
        private MockGroundDetectionModel _groundDetectionModel;
        private MockPullLimitModel _pullLimitModel;
        private MockScreenScaleModel _screenScaleModel;
        private MockKickPositionModel _kickPositionModel;
        private MockPlayerStateEntity _stateEntity;
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
            _stateEntity = new MockPlayerStateEntity(PlayerStateType.Idle);
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
        public async Task StateUpdate_NotGrounded_ChangesStateToFrying()
        {
            // Arrange
            _rayCasterView.HitsToReturn = Array.Empty<RaycastHit2D>(); // Not grounded

            // Act
            _controller.StateUpdate(0.1f);
            await Task.Delay(TimeSpan.FromSeconds(0.25));

            // Assert
            Assert.AreEqual(PlayerStateType.Frying, _stateEntity.CurrentState);
        }

        [Test]
        public async Task StateUpdate_IsAiming_ChangesStateToAiming()
        {
            // Arrange
            var hit = new RaycastHit2D { normal = Vector2.up };
            _rayCasterView.HitsToReturn = new[] { hit }; // Grounded
            var dragDelta = new Vector2(0, _screenScaleModel.Height * _pullLimitModel.CancelRatio * 1.1f);
            _touchView.DraggingInfo =
                Option<FingerDraggingInfo>.Some(new FingerDraggingInfo(Vector2.zero, dragDelta, Vector2.zero));

            // Act
            _controller.StateUpdate(0.1f);
            await Task.Delay(TimeSpan.FromSeconds(0.25));

            // Assert
            Assert.AreEqual(PlayerStateType.Aiming, _stateEntity.CurrentState);
        }

        [Test]
        public void OnDoubleTap_WithPositionToUndo_ResetsPlayerPosition()
        {
            // Arrange
            var undoPosition = new Vector2(5, 5);
            var pose = new Pose(undoPosition, Quaternion.identity);
            _kickPositionModel.SetPositionToPop(pose);

            // Act
            _doubleTapView.SimulateDoubleTap();

            // Assert
            Assert.IsTrue(_playerView.ResetPositionValue.HasValue);
            Assert.AreEqual(undoPosition, _playerView.ResetPositionValue.Value);
        }
    }
}