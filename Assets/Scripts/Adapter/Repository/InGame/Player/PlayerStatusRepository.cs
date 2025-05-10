using Adapter.IDataStore.InGame.Player;
using Domain.IRepository.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class BasePowerRepository : IKickBasePowerRepository
    {
        public BasePowerRepository
        (
            IPlayerKickStatusDataStore kickStatusDataStore
        )
        {
            KickStatusDataStore = kickStatusDataStore;
        }

        public float KickBasePower => KickStatusDataStore.LoadKickStatus(0);
        private IPlayerKickStatusDataStore KickStatusDataStore { get; }
    }
}