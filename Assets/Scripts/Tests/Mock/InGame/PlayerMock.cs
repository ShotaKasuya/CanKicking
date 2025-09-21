using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Model.InGame;
using Interface.View.InGame;
using Module.Option.Runtime;
using R3;
using Structure.InGame.Player;
using Structure.Utility;
using UnityEngine;

namespace Tests.Mock.InGame
{
    //==============================================================================================
    // View
    //==============================================================================================
    public class MockCanKickView : ICanKickView
    {
        public Vector2 Direction;
        public float RotationPower;

        public void Kick(KickContext context)
        {
            Direction = context.Direction;
            RotationPower = context.RotationPower;
        }
    }

    public class MockPlayerView : IPlayerView, IPlayerCommandReceiver
    {
        private readonly Subject<Collision2D> _collisionSubject = new();
        private readonly Subject<IPlayerInteractCommand> _commandSubject = new();

        public Transform ModelTransform { get; } = new GameObject("MockPlayer").transform;
        public Vector2 LinearVelocity { get; set; }
        public float AngularVelocity { get; set; }
        public Observable<Collision2D> CollisionEnterEvent => _collisionSubject;
        public Vector2? ResetPositionValue { get; private set; }
        public bool IsActive { get; private set; }

        public void Activation(bool isActive)
        {
            IsActive = isActive;
        }

        public void ResetPosition(Pose pose)
        {
            ResetPositionValue = pose.position;
            ModelTransform.position = pose.position;
            ModelTransform.rotation = pose.rotation;
        }

        public void SimulateCollision(Collision2D collision) => _collisionSubject.OnNext(collision);

        // IPlayerCommandReceiver
        public void SendCommand(IPlayerInteractCommand command) => _commandSubject.OnNext(command);
        public Observable<IPlayerInteractCommand> Stream => _commandSubject;
    }

    public class MockAimView : IAimView
    {
        public Vector2 AimVector { get; private set; }
        public bool IsShown { get; private set; }
        public void SetAim(Vector2 aimVector) => AimVector = aimVector;
        public void Show() => IsShown = true;
        public void Hide() => IsShown = false;
    }

    public class MockRayCasterView : IRayCasterView
    {
        public RaycastHit2D[] HitsToReturn = Array.Empty<RaycastHit2D>();
        public ReadOnlySpan<RaycastHit2D> PoolRay(RayCastInfo rayCastInfo) => new(HitsToReturn);
    }

    public class MockSpawnEffectView : ISpawnEffectView
    {
        public bool IsInitialized { get; private set; }
        public int SpawnEffectCallCount { get; private set; }

        public UniTask Initialize(CancellationToken token)
        {
            IsInitialized = true;
            return UniTask.CompletedTask;
        }

        public UniTask SpawnEffect(Vector2 spawnPoint, Vector2 angle, float duration,
            CancellationToken cancellationToken)
        {
            SpawnEffectCallCount++;
            return UniTask.CompletedTask;
        }
    }

    //==============================================================================================
    // Model
    //==============================================================================================

    public class MockKickPositionModel : IKickPositionModel
    {
        private Pose? _positionToPop;
        public Pose? PushedPosition { get; private set; }

        public void SetPositionToPop(Pose? pos) => _positionToPop = pos;

        public Option<Pose> PopPosition() => _positionToPop.HasValue
            ? Option<Pose>.Some(_positionToPop.Value)
            : Option<Pose>.None();

        public void PushPosition(Pose pose)
        {
            PushedPosition = pose;
        }
    }

    public class MockGroundDetectionModel : IGroundDetectionModel
    {
        public RayCastInfo GroundDetectionInfo => new(Vector2.down, 1f, 1);
        public float MaxSlope { get; set; } = 45f;
    }

    public class MockPullLimitModel : IPullLimitModel
    {
        public float CancelRatio { get; set; } = 0.1f;
        public float MaxRatio { get; set; } = 1.0f;
    }

    public class MockEffectSpawnModel : IEffectSpawnModel
    {
        public float SpawnThreshold { get; set; } = 5f;
        public float EffectLength { get; set; } = 1f;
    }
}