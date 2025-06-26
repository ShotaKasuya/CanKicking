using System;
using Interface.Global.TimeScale;
using Module.EnumArray;
using Structure.Global.TimeScale;
using UnityEngine;

namespace Model.Global
{
    [Serializable]
    public class TimeScaleModel : ITimeScaleModel
    {
        [SerializeField] [EnumArray(typeof(TimeCommandType))]
        private EnumArray<float> timeScaleSettings;

        private float _prevTimeScale;

        public void Execute(TimeCommandType timeCommand)
        {
            _prevTimeScale = Time.timeScale;
            Time.timeScale = timeScaleSettings.Get((int)timeCommand);
        }

        public void Undo()
        {
            if (_prevTimeScale == 0f)
            {
                Time.timeScale = timeScaleSettings.Get((int)TimeCommandType.Normal);
                return;
            }

            Time.timeScale = _prevTimeScale;
            _prevTimeScale = 0;
        }
    }
}