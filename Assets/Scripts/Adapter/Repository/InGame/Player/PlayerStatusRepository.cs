using Domain.IRepository.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class PlayerStatusRepository : IMutPlayerKickStatusRepository
    {
        public PlayerStatusRepository
        (
            
        )
        {
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

        private float _kickBasePower = 5;
        private float _kickMaxPower = 10;
    }
}