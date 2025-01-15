using System;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;

namespace Domain.UseCase.InGame.Player
{
    public class KickCase : IDisposable
    {
        public KickCase
        (
            IKickPresenter kickPresenter,
            IFingerReleaseEventPresenter fingerReleaseEventPresenter,
            IKickPowerRepository kickPowerRepository,
            IPlayerKickStatusRepository playerKickStatusRepository
        )
        {
            KickPresenter = kickPresenter;
            ReleaseEventPresenter = fingerReleaseEventPresenter;
            KickPowerRepository = kickPowerRepository;
            KickStatusRepository = playerKickStatusRepository;

            ReleaseEventPresenter.ReleaseEvent += OnKick;
        }

        private const float BaseKickPower = 1;

        private void OnKick(FingerReleaseEventArg eventArg)
        {
            // todo filter event

            // todo calc power
            var currentPower = KickPowerRepository.CurrentPower + BaseKickPower;
            var kickVector = -eventArg.FingerDelta.normalized;  // 引っ張って飛ばすため、向きを反転させる

            var power = KickStatusRepository.KickBasePower + KickStatusRepository.KickMaxPower * currentPower;
            var torque = kickVector.x;

            var kickArg = new KickArg(power, kickVector, torque);
            KickPresenter.Kick(kickArg);
        }

        private IKickPowerRepository KickPowerRepository { get; }
        private IPlayerKickStatusRepository KickStatusRepository { get; }
        private IKickPresenter KickPresenter { get; }
        private IFingerReleaseEventPresenter ReleaseEventPresenter { get; }

        public void Dispose()
        {
            ReleaseEventPresenter.ReleaseEvent -= OnKick;
        }
    }
}