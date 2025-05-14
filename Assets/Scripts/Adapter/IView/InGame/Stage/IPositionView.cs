using UnityEngine;

namespace Adapter.IView.InGame.Stage
{
    public interface ISpawnPositionView
    {
        public Pose Position { get; }
    }
}