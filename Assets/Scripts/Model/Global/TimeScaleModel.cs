using System;
using Interface.Model.Global;
using Module.EnumArray.Runtime;
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

        public void Reset()
        {
            var defaultScale = timeScaleSettings.Get((int)TimeCommandType.Normal);
            Time.timeScale = defaultScale;
            _prevTimeScale = defaultScale;
        }
    }
}