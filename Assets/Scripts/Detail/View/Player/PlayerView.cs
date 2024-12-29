using Adapter.IView.Player;
using Module.ImmutableClass;
using UnityEngine;

namespace Detail.View.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerView : MonoBehaviour, IMutPlayerView
    {
        public Transform ModelTransform => _modelTransform;
        public Rigidbody2D MutRbody => _rbody;
        public ConstRbody Rbody => _constRbody;

        public Pose PlayerPose => new Pose(_modelTransform.position, _modelTransform.rotation);

        private Transform _modelTransform;
        private Rigidbody2D _rbody;
        private ConstRbody _constRbody;

        private void Awake()
        {
            _modelTransform = transform;
            _rbody = GetComponent<Rigidbody2D>();
            _constRbody = new ConstRbody(_rbody);
        }
    }
}