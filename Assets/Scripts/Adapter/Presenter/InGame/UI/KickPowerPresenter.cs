using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class KickPowerPresenter: IKickPowerPresenter
    {
        public KickPowerPresenter
        (
            IPowerView powerView
        )
        {
            PowerView = powerView;
        }
        
        public void ToggleOn()
        {
            PowerView.Enable();
        }

        public void ToggleOff()
        {
            PowerView.Disable();
        }

        public void ShowPower(ShowKickPowerArg arg)
        {
            PowerView.SetPower(arg.Power);
        }
        
        private IPowerView PowerView { get; }
    }
}