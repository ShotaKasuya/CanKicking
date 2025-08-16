using System;
using Interface.InGame.Player;
using R3;
using R3.Triggers;
using Structure.InGame.Player;
using Structure.Utility;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPlayerView, ICanKickView, IRayCasterView, IPlayerCommandReceiver
    {
        public Transform ModelTransform => _modelTransform!;
        public Vector2 LinearVelocity => _rigidbody.linearVelocity;
        public float AngularVelocity => _rigidbody.angularVelocity;
        public Observable<Collision2D> CollisionEnterEvent => this.OnCollisionEnter2DAsObservable();
        public Observable<PlayerInteractCommand> Stream => CommandSubject;

        private GameObject _self;
        private Transform _modelTransform;
        private Rigidbody2D _rigidbody;
        private RaycastHit2D[] _raycastPool;
        private Subject<PlayerInteractCommand> CommandSubject { get; } = new();

        [SerializeField] private int raycastPoolSize;

        private void Awake()
        {
            _self = gameObject;
            _modelTransform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _raycastPool = new RaycastHit2D[raycastPoolSize];
            CommandSubject.AddTo(this);
        }

        public void Activation(bool isActive)
        {
            _self.SetActive(isActive);
        }

        public void ResetPosition(Vector2 position)
        {
            ModelTransform.position = position;
            _rigidbody.linearVelocity = Vector2.zero;
        }

        public void Kick(KickContext context)
        {
            _rigidbody.AddForce(context.Direction, ForceMode2D.Impulse);
            _rigidbody.AddTorque(context.RotationPower, ForceMode2D.Impulse);
        }

        public ReadOnlySpan<RaycastHit2D> PoolRay(RayCastInfo rayCastInfo)
        {
            Vector2 position = _modelTransform!.position;
            var hitCount = Physics2D.RaycastNonAlloc
            (
                position,
                rayCastInfo.Direction,
                _raycastPool,
                rayCastInfo.Distance,
                rayCastInfo.LayerMask
            );
            return _raycastPool.AsSpan(0, hitCount);
        }

        public void SendCommand(PlayerInteractCommand playerInteractCommand)
        {
            CommandSubject.OnNext(playerInteractCommand);
        }
    }
}