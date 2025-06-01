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
        }


        public Vector2 ScreenScale => new Vector2(Screen.width, Screen.height);
        public ReadOnlyReactiveProperty<float> ReactiveWidthWeight => ScreenWidth;
        public void SetWeight(float weight)
        {
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