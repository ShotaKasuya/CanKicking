using UnityEngine;

namespace Domain.IPresenter.InGame.Stage
{
    public interface ISpawnPositionPresenter
    {
        public Vector3 SpawnPosition { get; }
    }
}