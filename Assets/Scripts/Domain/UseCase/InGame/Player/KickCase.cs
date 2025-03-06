using System;
using DataUtil.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;
using Module.StateMachine;

namespace Domain.UseCase.InGame.Player
{
    public class KickCase : IStateBehaviour<PlayerStateType>, IDisposable
    {
        public KickCase
        (
            PlayerStateType kickableState,
            IKickPresenter kickPresenter,
            IFingerReleaseEventPresenter fingerReleaseEventPresenter,
            IKickPowerRepository kickPowerRepository,
            IPlayerKickStatusRepository playerKickStatusRepository
        )
        {
            TargetStateMask = kickableState;
            KickPresenter = kickPresenter;
            ReleaseEventPresenter = fingerReleaseEventPresenter;
            KickPowerRepository = kickPowerRepository;
            KickStatusRepository = playerKickStatusRepository;

            ReleaseEventPresenter.ReleaseEvent += OnKick;
        }

        private void OnKick(FingerReleaseEventArg eventArg)
        {
            // todo calc power
            var kickVector = -eventArg.FingerDelta.normalized; // 引っ張って飛ばすため、向きを反転させる

            var power = KickStatusRepository.KickBasePower.BasePower;
            var torque = kickVector.x;

            var kickArg = new KickArg(power, kickVector, torque);
            KickPresenter.Kick(kickArg);
        }

        public void OnEnter()
        {
            ReleaseEventPresenter.ReleaseEvent += OnKick;
        }

        public void OnExit()
        {
            ReleaseEventPresenter.ReleaseEvent -= OnKick;
        }

        public void StateUpdate(float deltaTime)
        {
        }

        public PlayerStateType TargetStateMask { get; }
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