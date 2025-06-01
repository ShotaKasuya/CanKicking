using R3;
using UnityEngine;

namespace Domain.IRepository.Util
{
    public interface IScreenScaleRepository
    {
        public Vector2 ScreenScale { get; }
    }

    public interface IScreenWidthRepository
    {
        public float WidthWeight => ReactiveWidthWeight.CurrentValue;
        public ReadOnlyReactiveProperty<float> ReactiveWidthWeight { get; }
        public void SetWeight(float weight);
        public float GetScreenWidth(float weight);
    }
}