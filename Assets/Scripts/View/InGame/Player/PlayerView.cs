using System;
using Interface.InGame.Player;
using R3;
using Structure.Utility;
using UnityEngine;

namespace View.InGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IPlayerView, ICanKickView, IRayCasterView
    {
        public Transform ModelTransform => _modelTransform!;
        public Vector2 LinearVelocity => _rigidbody!.linearVelocity;
        public float AngularVelocity => _rigidbody!.angularVelocity;
        public Observable<Collision2D> CollisionEnterEvent => _collisionEnterSubject;

        private Transform _modelTransform;
        private Rigidbody2D _rigidbody;
        private Subject<Collision2D> _collisionEnterSubject = new Subject<Collision2D>();
        private RaycastHit2D[] _raycastPool;

        [SerializeField] private int raycastPoolSize;

        private void Awake()
        {
            _modelTransform = transform;
            _rigidbody = GetComponent<Rigidbody2D>();
            _collisionEnterSubject = new Subject<Collision2D>();
            _raycastPool = new RaycastHit2D[raycastPoolSize];
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            _collisionEnterSubject.OnNext(other);
        }

        public void Kick(KickContext context)
        {
            _rigidbody!.AddForce(context.Direction, ForceMode2D.Impulse);
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
    }
}