using UnityEngine;

namespace Domain.IPresenter.InGame.Stage
{
    public interface ISpawnPresenter
    {
        public void Spawn(SpawnArg arg);
    }

    public struct SpawnArg
    {
        public SpawnArg(Vector3 position)
        {
            Position = position;
        }
        
        public Vector3 Position { get; }
    }
}