using Adapter.IDataStore.Util;
using UnityEngine;

namespace Adapter.DataStore.Setting
{
    [CreateAssetMenu(fileName = "PlayerSettings", menuName = "PlayerSettings", order = 0)]
    public class PlayerScreenDataStore : ScriptableObject, IScreenDataStore
    {
        [SerializeField] private float screenWidth;

        public float ScreenWidth => screenWidth;
        
        
        public void ScreenWidthStore(float width)
        {
            screenWidth = width;
        }
    }
}