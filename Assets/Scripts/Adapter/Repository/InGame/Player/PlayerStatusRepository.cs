using Adapter.IDataStore.InGame.Player;
using Domain.IRepository.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class PlayerStatusRepository : IMutPlayerKickStatusRepository
    {
        public PlayerStatusRepository
        (
            IPlayerKickStatusDataStore kickStatusDataStore
        )
        {
            var status = kickStatusDataStore.LoadKickStatus(0);

            _kickBasePower = status.KickBasePower;
            _kickMaxPower = status.KickMaxPower;
        }

        public float KickBasePower => _kickBasePower;
        public float KickMaxPower => _kickMaxPower;
        public void SetKickBasePower(float power)
        {
            _kickBasePower = power;
        }

        public void SetKickMaxPower(float power)
        {
            _kickMaxPower = power;
        }

        private float _kickBasePower;
        private float _kickMaxPower;
    }
}