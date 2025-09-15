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
using Tests.Mock.InGame.Primary;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class AimingControllerTest
    {
        private AimingController _controller;
        private MockTouchView _touchView;
        private MockPlayerView _playerView;
        private MockAimView _aimView;
        private MockCanKickView _canKickView;
        private MockSeSourceView _seSourceView;
        private MockKickPositionModel _kickPositionModel;
        private MockKickBasePowerModel _kickBasePowerModel;
        private MockKickCountModel _kickCountModel;
        private MockPlayerSoundModel _playerSoundModel;
        private MockCalcKickPowerLogic _calcKickPowerLogic;
        private MockPlayerStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _touchView = new MockTouchView();
            _playerView = new MockPlayerView();
            _aimView = new MockAimView();
            _canKickView = new MockCanKickView();
            _seSourceView = new MockSeSourceView();
            _kickPositionModel = new MockKickPositionModel();
            _kickBasePowerModel = new MockKickBasePowerModel();
            _kickCountModel = new MockKickCountModel();
            _playerSoundModel = new MockPlayerSoundModel();
            _calcKickPowerLogic = new MockCalcKickPowerLogic();
            _stateEntity = new MockPlayerStateEntity(PlayerStateType.Aiming);
            _compositeDisposable = new CompositeDisposable();

            _controller = new AimingController(
                _touchView, _playerView, _aimView, _canKickView, _seSourceView,
                _kickPositionModel, _kickBasePowerModel, _kickCountModel,
                _playerSoundModel, _calcKickPowerLogic, _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public async Task StateUpdate_NoDragging_ChangesStateToIdle()
        {
            _touchView.DraggingInfo = Option<FingerDraggingInfo>.None();
            _controller.StateUpdate(0.1f);

            await Task.Delay(TimeSpan.FromSeconds(0.25));

            Assert.AreEqual(PlayerStateType.Idle, _stateEntity.CurrentState);
        }

        [Test]
        public void OnEnter_ShowsAimView()
        {
            _controller.OnEnter();
            Assert.IsTrue(_aimView.IsShown);
        }

        [Test]
        public void OnExit_HidesAimView()
        {
            _controller.OnExit();
            Assert.IsFalse(_aimView.IsShown);
        }

        [Test]
        public async Task Jump_OnTouchEnd_PerformsKickAndChangesState()
        {
            // Arrange
            var touchEndArg = new TouchEndEventArgument(Vector2.zero, new Vector2(100, 0));
            var kickPower = new Vector2(0.5f, 0.5f);
            _calcKickPowerLogic.PowerToReturn = kickPower;

            // Act
            _touchView.SimulateTouchEnd(touchEndArg);
            await Task.Delay(TimeSpan.FromSeconds(0.25));

            // Assert
            Assert.AreEqual(kickPower * _kickBasePowerModel.KickPower, _canKickView.Direction);
            Assert.AreEqual(_playerSoundModel.KickSound, _seSourceView.PlayedClip);
            Assert.IsNotNull(_kickPositionModel.PushedPosition);
            Assert.AreEqual(1, _kickCountModel.Count);
            Assert.AreEqual(PlayerStateType.Frying, _stateEntity.CurrentState);
        }
    }
}