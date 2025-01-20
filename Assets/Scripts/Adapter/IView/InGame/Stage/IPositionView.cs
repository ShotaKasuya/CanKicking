using UnityEngine;

namespace Adapter.IView.InGame.Stage
{
    public interface IPositionView
    {
        public Pose Position { get; }
    }
    public interface IMutPositionView: IPositionView
    {
        public Transform MutPosition { get; }
    }
}