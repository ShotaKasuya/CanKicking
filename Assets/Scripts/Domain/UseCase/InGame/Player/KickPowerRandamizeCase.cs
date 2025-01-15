using DataUtil.InGame.Player;
using DataUtil.Util.Installer;
using Domain.IRepository.InGame.Player;
using UnityEngine;

namespace Domain.UseCase.InGame.Player
{
    public class KickPowerRandomizationCase: ITickable
    {
        public KickPowerRandomizationCase
        (
            IMutKickPowerRepository kickPowerRepository,
            KickRandomConfig randomConfig
        )
        {
            KickPowerRepository = kickPowerRepository;
            RandomConfig = randomConfig;
        }
        
        public void Tick(float deltaTime)
        {
            var currentPower = KickPowerRepository.CurrentPower;
            if (Mathf.Approximately(_currentTarget, currentPower))
            {
                _currentTarget = Random.Range(IMutKickPowerRepository.Min, IMutKickPowerRepository.Max);
            }

            var delta = RandomConfig.Speed * deltaTime;
            var power = Mathf.MoveTowards(currentPower, _currentTarget, delta);
            KickPowerRepository.SetPower(power);
        }

        private float _currentTarget;
        private IMutKickPowerRepository KickPowerRepository { get; }
        private KickRandomConfig RandomConfig { get; }
    }
}