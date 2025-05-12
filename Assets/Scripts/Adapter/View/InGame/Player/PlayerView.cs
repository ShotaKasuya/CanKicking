using System;
using Adapter.IView.InGame.Player;
using Module.ImmutableClass;
using UnityEngine;

namespace Adapter.View.InGame.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IMutPlayerView, IPlayerCastView, IContactView
    {
        public Transform ModelTransform => _modelTransform;
        public Rigidbody2D MutRbody => _rbody;
        public ConstRbody Rbody => _constRbody;

        public Pose PlayerPose => new Pose(_modelTransform.position, _modelTransform.rotation);
        public Action<Collision2D> ContactEvent { get; set; }

        private Transform _modelTransform;
        private Rigidbody2D _rbody;
        private ConstRbody _constRbody;

        private void Awake()
        {
            _modelTransform = transform;
            _rbody = GetComponent<Rigidbody2D>();
            _constRbody = new ConstRbody(_rbody);
        }

        public int CastFromPlayer(RayCastInfo rayCastInfo, RaycastHit2D[] result)
        {
            return Physics2D.RaycastNonAlloc(
                ModelTransform.position,
                rayCastInfo.Direction,
                result, rayCastInfo.Distance,
                rayCastInfo.LayerMask
            );
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            ContactEvent?.Invoke(other);
        }
    }
}