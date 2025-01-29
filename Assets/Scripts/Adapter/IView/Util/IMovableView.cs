using UnityEngine;

namespace Adapter.IView.Util
{
    public interface IMovableView
    {
        public float Damping { get; }
        public void Translate(Vector2 moveTo);
    }
}