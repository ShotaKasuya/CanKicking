using System;
using Domain.IPresenter.InGame.UI;
using Domain.IPresenter.Util.Input;
using Domain.IRepository.InGame.Player;

namespace Domain.UseCase.InGame.UI
{
    public class ShowPowerCase: IDisposable
    {
        public ShowPowerCase
        (
            IKickPowerPresenter kickPowerPresenter,
            IFingerTouchEventPresenter touchEventPresenter,
            IFingerTouchingEventPresenter touchingEventPresenter,
            IFingerReleaseEventPresenter releaseEventPresenter,
            IKickPowerRepository kickPowerRepository
        )
        {
            KickPowerPresenter = kickPowerPresenter;
            TouchEventPresenter = touchEventPresenter;
            TouchingEventPresenter = touchingEventPresenter;
            ReleaseEventPresenter = releaseEventPresenter;
            KickPowerRepository = kickPowerRepository;

            touchEventPresenter.TouchEvent += _ => ToggleOn();
            touchingEventPresenter.TouchingEvent += ShowPower;
            releaseEventPresenter.ReleaseEvent += _ => ToggleOff();
        }

        private void ToggleOn()
        {
            KickPowerPresenter.ToggleOn();
        }

        private void ShowPower(FingerTouchingEventArg eventArg)
        {
            var from = eventArg.StartPosition;
            var to = eventArg.CurrentTouchPosition;
            var power = KickPowerRepository.CurrentPower;
            
            var arg = new ShowKickPowerArg(power, from, to);
            KickPowerPresenter.ShowPower(arg);
        }

        private void ToggleOff()
        {
            KickPowerPresenter.ToggleOff();
        }

        private IKickPowerPresenter KickPowerPresenter { get; }
        private IFingerTouchEventPresenter TouchEventPresenter { get; }
        private IFingerTouchingEventPresenter TouchingEventPresenter { get; }
        private IFingerReleaseEventPresenter ReleaseEventPresenter { get; }
        private IKickPowerRepository KickPowerRepository { get; }

        public void Dispose()
        {
            TouchEventPresenter.TouchEvent -= _ => ToggleOn();
            TouchingEventPresenter.TouchingEvent -= ShowPower;
            ReleaseEventPresenter.ReleaseEvent -= _ => ToggleOff();
        }
    }
}