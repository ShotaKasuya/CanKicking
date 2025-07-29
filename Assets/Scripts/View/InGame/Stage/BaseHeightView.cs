using Interface.InGame.Stage;
using UnityEngine;

namespace View.InGame.Stage
{
    public class BaseHeightView: MonoBehaviour, IBaseHeightView
    {
        public float PositionY => baseHeight;

        [SerializeField] private float baseHeight;

        private void Awake()
        {
            baseHeight = transform.position.y;
        }

        private void Reset()
        {
            baseHeight = transform.position.y;
        }
    }
}