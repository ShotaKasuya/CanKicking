using Interface.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    public class PlayerSpawnPositionView: MonoBehaviour, IPlayerSpawnPositionView
    {
        public Transform StartPosition => _selfTransform;
        private Transform _selfTransform;

        private void Awake()
        {
            _selfTransform = transform;
        }
    }
}