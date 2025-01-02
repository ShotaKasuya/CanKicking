using System;
using DataUtil.InGame.Player;
using DataUtil.Util.Input;
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

        private void OnKick(FingerReleaseEventArg eventArg)
        {
            // todo filter event

            // todo calc power
            var currentPower = KickPowerRepository.CurrentPower;
            var kickVector = -eventArg.FingerDelta.normalized;  // 引っ張って飛ばすため、向きを反転させる

            var power = KickStatusRepository.KickBasePower + KickStatusRepository.KickMaxPower * currentPower;
            var kickPower = kickVector * power;
            var torque = kickVector.x;

            var kickArg = new KickArg(kickPower, torque);
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