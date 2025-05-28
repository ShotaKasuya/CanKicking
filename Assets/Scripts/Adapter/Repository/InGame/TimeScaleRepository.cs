using Adapter.IDataStore.InGame;
using Domain.IRepository.InGame;

namespace Adapter.Repository.InGame
{
    public class TimeScaleRepository: IMutTimeScaleRepository, ITimeScaleRepository
    {
        public TimeScaleRepository
        (
            ITimeScaleDataStore timeScaleDataStore
        )
        {
            TimeScaleDataStore = timeScaleDataStore;

            var data = timeScaleDataStore.LoadTimeScales();
            FryState = data.FryState;
        }
        
        public float FryState { get; private set; }
        public void SetFryStateTimeScale(float timeScale)
        {
            FryState = timeScale;
        }
        
        private ITimeScaleDataStore TimeScaleDataStore { get; }
    }
}