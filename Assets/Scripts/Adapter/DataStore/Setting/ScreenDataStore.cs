using Adapter.IDataStore.Util;
using UnityEngine;

namespace Adapter.DataStore.Setting
{
    [CreateAssetMenu(fileName = "ScreenDataStore", menuName = "ScreenDataStore", order = 0)]
    public class ScreenDataStore : ScriptableObject, IScreenDataStore
    {
        [SerializeField] private float minWidth;
        [SerializeField] private float maxWidth;
        [SerializeField, HideInInspector] private float savedWeight;


        public float MinWidth => minWidth;
        public float MaxWidth => maxWidth;

        public void StoreWeight(float weight)
        {
            savedWeight = weight;
        }
    }
}