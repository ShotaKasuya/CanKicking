using Adapter.IDataStore.InGame.Player;
using DataUtil.InGame.Player;
using Domain.IRepository.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class PlayerStatusRepository : IPlayerKickStatusRepository, IKickableSpeedRepository
    {
        public PlayerStatusRepository
        (
            IPlayerKickStatusDataStore kickStatusDataStore,
            IKickableSpeedDataStore kickableSpeedDataStore
        )
        {
            KickStatusDataStore = kickStatusDataStore;
            KickableSpeedDataStore = kickableSpeedDataStore;
            
            KickBasePower = kickStatusDataStore.LoadKickStatus(0);
            SqrKickableVelocity = kickableSpeedDataStore.KickableSpeed * kickableSpeedDataStore.KickableSpeed;
        }

        public KickPower KickBasePower { get; }
        public float SqrKickableVelocity { get; }
        
        private IPlayerKickStatusDataStore KickStatusDataStore { get; }
        private IKickableSpeedDataStore KickableSpeedDataStore { get; }
    }
}