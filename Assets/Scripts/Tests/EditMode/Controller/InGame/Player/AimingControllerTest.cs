using Controller.InGame.Player;
using Cysharp.Threading.Tasks;
using Interface.Global.Audio;
using Interface.Global.Input;
using Interface.InGame.Player;
using Interface.InGame.Primary;
using Module.Option.Runtime;
using Module.StateMachine;
using NUnit.Framework;
using R3;
using Structure.InGame.Player;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Player
{
    public class AimingControllerTest
    {
        // Mocks
        private class MockTouchView : ITouchView
        {
            private readonly Subject<TouchEndEventArgument> _touchEndSubject = new();
            public Observable<TouchStartEventArgument> TouchEvent => Observable.Empty<TouchStartEventArgument>();

            public Option<FingerDraggingInfo> DraggingInfo { get; set; } = Option<FingerDraggingInfo>.None();

            public Observable<TouchEndEventArgument> TouchEndEvent => _touchEndSubject;
            public void SimulateTouchEnd(TouchEndEventArgument arg) => _touchEndSubject.OnNext(arg);
        }

        private class MockPlayerView : IPlayerView
        {
            public Transform ModelTransform { get; } = new GameObject().transform;
            public Vector2 LinearVelocity => Vector2.zero;
            public float AngularVelocity => 0f;
            public Observable<Collision2D> CollisionEnterEvent => Observable.Empty<Collision2D>();

            public void Activation(bool isActive)
            {
            }

            public void ResetPosition(Vector2 position)
            {
            }
        }

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

        private class MockKickPositionModel : IKickPositionModel
        {
            public Vector2? PushedPosition { get; private set; }
            public Option<Vector2> PopPosition() => Option<Vector2>.None();
            public void PushPosition(Vector2 position) => PushedPosition = position;
        }

        private class MockKickBasePowerModel : IKickBasePowerModel
        {
            public float KickPower { get; set; } = 10f;
            public float RotationPower { get; set; } = 1f;
        }

        private class MockJumpCountModel : IJumpCountModel, IResetable
        {
            public int Count { get; private set; }
            private readonly ReactiveProperty<int> _jumpCount = new(0);
            public ReadOnlyReactiveProperty<int> JumpCount => _jumpCount;

            public void Inc()
            {
                Count++;
                _jumpCount.Value++;
            }

            public void Reset()
            {
                Count = 0;
                _jumpCount.Value = 0;
            }
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

        private class MockStateEntity : IMutStateEntity<PlayerStateType>
        {
            public PlayerStateType CurrentState { get; private set; } = PlayerStateType.Aiming;
            public PlayerStateType EntryState => PlayerStateType.Idle;
            public Observable<PlayerStateType> StateExitObservable => Observable.Empty<PlayerStateType>();
            public Observable<PlayerStateType> StateEnterObservable => Observable.Empty<PlayerStateType>();

            public bool IsInState(PlayerStateType state)
            {
                return CurrentState == state;
            }

            public UniTask ChangeState(PlayerStateType next)
            {
                CurrentState = next;
                return UniTask.CompletedTask;
            }

            public OperationHandle GetStateLock(string context)
            {
                return new OperationHandle();
            }
        }

        private AimingController _controller;
        private MockTouchView _touchView;
        private MockPlayerView _playerView;
        private MockAimView _aimView;
        private MockCanKickView _canKickView;
        private MockSeSourceView _seSourceView;
        private MockKickPositionModel _kickPositionModel;
        private MockKickBasePowerModel _kickBasePowerModel;
        private MockJumpCountModel _jumpCountModel;
        private MockPlayerSoundModel _playerSoundModel;
        private MockCalcKickPowerLogic _calcKickPowerLogic;
        private MockStateEntity _stateEntity;
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
            _jumpCountModel = new MockJumpCountModel();
            _playerSoundModel = new MockPlayerSoundModel();
            _calcKickPowerLogic = new MockCalcKickPowerLogic();
            _stateEntity = new MockStateEntity();
            _compositeDisposable = new CompositeDisposable();

            _controller = new AimingController(
                _touchView, _playerView, _aimView, _canKickView, _seSourceView,
                _kickPositionModel, _kickBasePowerModel, _jumpCountModel,
                _playerSoundModel, _calcKickPowerLogic, _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void StateUpdate_NoDragging_ChangesStateToIdle()
        {
            _touchView.DraggingInfo = Option<FingerDraggingInfo>.None();
            _controller.StateUpdate(0.1f);
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
        public void Jump_OnTouchEnd_PerformsKickAndChangesState()
        {
            // Arrange
            var touchEndArg = new TouchEndEventArgument(Vector2.zero, new Vector2(100, 0));
            var kickPower = new Vector2(0.5f, 0.5f);
            _calcKickPowerLogic.PowerToReturn = kickPower;

            // Act
            _touchView.SimulateTouchEnd(touchEndArg);

            // Assert
            Assert.AreEqual(kickPower * _kickBasePowerModel.KickPower, _canKickView.Direction);
            Assert.AreEqual(_playerSoundModel.KickSound, _seSourceView.PlayedClip);
            Assert.IsNotNull(_kickPositionModel.PushedPosition);
            Assert.AreEqual(1, _jumpCountModel.Count);
            Assert.AreEqual(PlayerStateType.Frying, _stateEntity.CurrentState);
        }
    }
}