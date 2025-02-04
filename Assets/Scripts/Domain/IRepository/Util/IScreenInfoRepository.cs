using System;
using UnityEngine;

namespace Domain.IRepository.Util
{
    public interface IScreenScaleRepository
    {
        public Vector2 ScreenScale { get; }
    }

    public interface IScreenWidthRepository
    {
        public float Width { get; }
        public Action OnWidthChange { get; set; }
    }
}