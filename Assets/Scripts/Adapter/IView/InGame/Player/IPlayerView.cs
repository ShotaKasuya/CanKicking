using Module.ImmutableClass;
using UnityEngine;

namespace Adapter.IView.InGame.Player
{
    public interface IPlayerView
    {
        public Pose PlayerPose { get; }
        public ConstRbody Rbody { get; }
    }

    public interface IMutPlayerView: IPlayerView
    {
        public Transform ModelTransform { get; }
        public Rigidbody2D MutRbody { get; }
    }
}