using System;
using Adapter.IDataStore.Util;
using Domain.IRepository.Util;
using UnityEngine;

namespace Adapter.Repository.Setting
{
    public class ScreenRepository: IScreenScaleRepository,IScreenWidthRepository
    {
        public ScreenRepository(IScreenDataStore screenDataStore)
        {
            ScreenDataStore = screenDataStore;
        }
        
        public Vector2 ScreenScale => new Vector2(Screen.width, Screen.height);
        public Action OnWidthChange { get; set; }
        public float Width => ScreenDataStore.ScreenWidth;
        
        private IScreenDataStore ScreenDataStore { get; }
    }
}