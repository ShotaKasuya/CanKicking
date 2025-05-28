using Adapter.IDataStore.InGame;
using UnityEngine;

namespace Adapter.DataStore.Setting
{
    [CreateAssetMenu(fileName = "TimeScaleSettings", menuName = "TimeScaleSettings", order = 0)]
    public class TimeScaleDataStore:ScriptableObject, ITimeScaleDataStore
    {
        [SerializeField] private float fryStateTimeScale;
        
        public TimeScaleSettings LoadTimeScales()
        {
            return new TimeScaleSettings(fryStateTimeScale);
        }

        public void StoreTimeScales(TimeScaleSettings scaleSettings)
        {
            fryStateTimeScale = scaleSettings.FryState;
        }
    }
}