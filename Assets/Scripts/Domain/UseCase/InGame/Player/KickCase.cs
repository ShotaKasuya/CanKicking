using System;
using DataUtil.InGame.Player;
using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;

namespace Domain.UseCase.InGame.Player
{
    public class KickCase : IDisposable
    {
        public KickCase
        (
            PlayerStateType kickableState,
            IPlayerStateEntity stateEntity,
            IKickPresenter kickPresenter,
            IFingerReleaseEventPresenter fingerReleaseEventPresenter,
            IKickPowerRepository kickPowerRepository,
            IPlayerKickStatusRepository playerKickStatusRepository
        )
        {
            KickableState = kickableState;
            PlayerStateEntity = stateEntity;
            KickPresenter = kickPresenter;
            ReleaseEventPresenter = fingerReleaseEventPresenter;
            KickPowerRepository = kickPowerRepository;
            KickStatusRepository = playerKickStatusRepository;

            ReleaseEventPresenter.ReleaseEvent += OnKick;
        }

        // FIXME?
        private const float BaseKickPower = 1;

        private void OnKick(FingerReleaseEventArg eventArg)
        {
            // todo filter event
            if (!PlayerStateEntity.IsInState(KickableState))
            {
                return;
            }

            // todo calc power
            var currentPower = KickPowerRepository.CurrentPower + BaseKickPower;
            var kickVector = -eventArg.FingerDelta.normalized;  // 引っ張って飛ばすため、向きを反転させる

            var power = KickStatusRepository.KickBasePower + KickStatusRepository.KickMaxPower * currentPower;
            var torque = kickVector.x;

            var kickArg = new KickArg(power, kickVector, torque);
            KickPresenter.Kick(kickArg);
        }

        private PlayerStateType KickableState { get; }
        private IPlayerStateEntity PlayerStateEntity { get; }
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