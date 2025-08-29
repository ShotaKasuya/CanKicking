using System;
using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.Player;
using Interface.Logic.InGame;
using Interface.Model.InGame;
using Interface.View.Global;
using Interface.View.InGame;
using Module.Option.Runtime;
using NUnit.Framework;
using R3;
using Structure.InGame.Player;
using Tests.Mock;
using Tests.Mock.Global;
using Tests.Mock.InGame.Player;
using Tests.Mock.InGame.Primary;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class AimingControllerTest
    {
        private class MockAimView : IAimView
        {
            public Vector2 AimVector { get; private set; }
            public bool IsShown { get; private set; }
            public void SetAim(Vector2 aimVector) => AimVector = aimVector;
            public void Show() => IsShown = true;
            public void Hide() => IsShown = false;
        }

        private class MockCanKickView : ICanKickView
        {
            public Vector2 Direction;
            public float RotationPower;

            public void Kick(KickContext context)
            {
                Direction = context.Direction;
                RotationPower = context.RotationPower;
            }
        }

        private class MockSeSourceView : ISeSourceView
        {
            public AudioClip PlayedClip { get; private set; }
            public void Play(AudioClip clip) => PlayedClip = clip;

            public void Stop()
            {
            }

            public void Continue()
            {
            }
        }

        private class MockKickBasePowerModel : IKickBasePowerModel
        {
            public float KickPower { get; set; } = 10f;
            public float RotationPower { get; set; } = 1f;
        }

        private class MockPlayerSoundModel : IPlayerSoundModel
        {
            public AudioClip KickSound = AudioClip.Create("Kick", 1, 1, 1000, false);
            public AudioClip BoundSound = AudioClip.Create("Bound", 1, 1, 1000, false);
            public AudioClip GetKickSound() => KickSound;
            public AudioClip GetBoundSound() => BoundSound;
        }

        private class MockCalcKickPowerLogic : ICalcKickPowerLogic
        {
            public Vector2 PowerToReturn { get; set; } = Vector2.one;
            public Vector2 CalcKickPower(Vector2 input) => PowerToReturn;
        }

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
        private MockStateEntity<PlayerStateType> _stateEntity;
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
            _stateEntity = new MockStateEntity<PlayerStateType>(PlayerStateType.Aiming);
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
        public async Task OnEnter_ShowsAimView()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.IsTrue(_aimView.IsShown);
        }

        [Test]
        public async Task OnExit_HidesAimView()
        {
            await _controller.OnExit(CancellationToken.None);
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