using Controller.InGame.Player;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Tests.Mock;
using Tests.Mock.Global;
using Tests.Mock.InGame;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class FryingControllerTest
    {
        private FryingController _controller;
        private MockPlayerView _playerView;
        private MockRayCasterView _rayCasterView;
        private MockGroundDetectionModel _groundDetectionModel;
        private MockTimeScaleModel _timeScaleModel;
        private MockPlayerStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _playerView = new MockPlayerView();
            _rayCasterView = new MockRayCasterView();
            _groundDetectionModel = new MockGroundDetectionModel();
            _timeScaleModel = new MockTimeScaleModel();
            _stateEntity = new MockPlayerStateEntity(PlayerStateType.Frying);
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

            Assert.AreEqual(PlayerStateType.Idle, _stateEntity.CurrentState);
        }
    }
}