using Adapter.IView.Util;
using UnityEngine;

namespace Detail.View.View
{
    public class MovableView: MonoBehaviour, IMovableView
    {
        [SerializeField] private float damping;
        public float Damping => damping;
        private Transform _modelTransform;

        private void Awake()
        {
            _modelTransform = transform;
        }

        public void Translate(Vector2 moveTo)
        {
            _modelTransform.Translate(moveTo);
        }
    }
}