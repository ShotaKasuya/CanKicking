using Adapter.IView.InGame.Stage;
using UnityEngine;

namespace Adapter.View.InGame.Stage
{
    public class SpawnPositionView: MonoBehaviour, ISpawnPositionView
    {
        public Pose Position => new Pose(_transform.position, _transform.rotation);
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }
    }
}