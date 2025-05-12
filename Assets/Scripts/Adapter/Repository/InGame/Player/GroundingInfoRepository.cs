using Adapter.IDataStore.InGame.Player;
using Domain.IRepository.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class GroundingInfoRepository: IGroundingInfoRepository
    {
        public GroundingInfoRepository(IGroundInfoDataStore groundInfoDataStore)
        {
            GroundInfoDataStore = groundInfoDataStore;
        }

        public float MaxSlope => GroundInfoDataStore.MaxSlope;
        
        private IGroundInfoDataStore GroundInfoDataStore { get; }
    }
}