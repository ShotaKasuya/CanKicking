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
        public float Width => ReactiveWidth.CurrentValue;
        public ReadOnlyReactiveProperty<float> ReactiveWidth { get; } 
    }
}