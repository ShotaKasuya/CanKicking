using System;
using Adapter.IDataStore.Util;
using Domain.IRepository.Util;
using R3;
using UnityEngine;

namespace Adapter.Repository.Setting
{
    public class ScreenRepository : IScreenScaleRepository, IScreenWidthRepository
    {
        public ScreenRepository(IScreenDataStore screenDataStore)
        {
            ScreenDataStore = screenDataStore;
            ScreenWidth = new ReactiveProperty<float>();

            ScreenScale = new Vector2(Screen.width, Screen.height);
        }

        public Vector2 ScreenScale { get; }
        public ReadOnlyReactiveProperty<float> ReactiveWidthWeight => ScreenWidth;

        public void SetWeight(float weight)
        {
            if (weight > 1f | weight < 0f)
            {
                throw new ArgumentOutOfRangeException();
            }

            ScreenWidth.Value = weight;
        }

        public float GetScreenWidth(float weight)
        {
            return (1 - weight) * ScreenDataStore.MinWidth + weight * ScreenDataStore.MaxWidth;
        }

        private IScreenDataStore ScreenDataStore { get; }
        private ReactiveProperty<float> ScreenWidth { get; }
    }
}