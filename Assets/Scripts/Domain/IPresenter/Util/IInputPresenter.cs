using Module.Option;
using UnityEngine;

namespace Domain.IPresenter.Util
{
    public interface ITouchEndPresenter
    {
        public Option<TouchEnd> Pool();
        public Option<TouchEnd> Peek();
    }

    public readonly struct TouchEnd
    {
        public Vector2 StartPoint { get; }
        public Vector2 EndPoint { get; }

        public TouchEnd
        (
            Vector2 startPoint,
            Vector2 endPoint
        )
        {
            StartPoint = startPoint;
            EndPoint = endPoint;
        }
    }
}