using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;

namespace Interface.InGame.UserInterface
{
    public interface INormalUiView
    {
        public UniTask Show();
        public UniTask Hide();
    }

    public interface IStopButtonView
    {
        public Observable<Unit> Performed { get; }
    }

    public interface IHeightUiView
    {
        public void SetHeight(float height);
    }

    public interface IPullRangeView
    {
        public void ShowRange(AimContext aimContext);
        public void HideRange();
    }

    public readonly ref struct AimContext
    {
        public AimContext
        (
            Vector2 startPoint,
            float cancelRadius,
            float maxRadius
        )
        {
            StartPoint = startPoint;
            CancelRadius = cancelRadius;
            MaxRadius = maxRadius;
        }

        public Vector2 StartPoint { get; }
        public float CancelRadius { get; }
        public float MaxRadius { get; }
    }

    public interface IRangeView
    {
        public void Set(Vector2 point, float radius);
        public void Hide();
    }
}