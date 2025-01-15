using System;
using Domain.IRepository.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class PowerRepository : IMutKickPowerRepository
    {
        public float CurrentPower { get; private set; } = 1;

        public void SetPower(float power)
        {
            if (power < 0 | power > 1)
            {
                throw new ArgumentOutOfRangeException();
            }

            CurrentPower = power;
        }
    }
}