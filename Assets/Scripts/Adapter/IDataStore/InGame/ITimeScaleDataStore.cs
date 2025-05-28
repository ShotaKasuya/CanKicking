namespace Adapter.IDataStore.InGame
{
    public interface ITimeScaleDataStore
    {
        public TimeScaleSettings LoadTimeScales();
        public void StoreTimeScales(TimeScaleSettings scaleSettings);
    }

    public readonly ref struct TimeScaleSettings
    {
        public float FryState { get; }

        public TimeScaleSettings
        (
            float fryState
        )
        {
            FryState = fryState;
        }
    }
}