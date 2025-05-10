using Adapter.IDataStore.Util;
using UnityEngine;

namespace Adapter.DataStore.Setting
{
    [CreateAssetMenu(fileName = "ScreenDataStore", menuName = "ScreenDataStore", order = 0)]
    public class ScreenDataStore : ScriptableObject, IScreenDataStore
    {
        [SerializeField] private float screenWidth;


        public float ScreenWidth => screenWidth;

        public void ScreenWidthStore(float width)
        {
            screenWidth = width;
        }
    }
}