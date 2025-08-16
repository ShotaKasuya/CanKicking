using Interface.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    public class SpawnPositionView: MonoBehaviour, ISpawnPositionView
    {
        public Transform StartPosition => _selfTransform;
        private Transform _selfTransform;

        private void Awake()
        {
            _selfTransform = transform;
        }
    }
}