using Interface.View.InGame;
using R3;
using Structure.InGame.Player;
using UnityEngine;

namespace Tests.Mock.InGame.Player
{
    public class MockPlayerView : IPlayerView, IPlayerCommandReceiver
    {
        private readonly Subject<Collision2D> _collisionSubject = new();
        private readonly Subject<PlayerInteractCommand> _commandSubject = new();

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
        public void SendCommand(PlayerInteractCommand command) => _commandSubject.OnNext(command);
        public Observable<PlayerInteractCommand> Stream => _commandSubject;
    }
}
