using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;

namespace Adapter.Presenter.InGame.Player
{
    public class AimPresenter: IAimPresenter
    {
        public AimPresenter
        (
            IAimView aimView
        )
        {
            AimView = aimView;
        }
        
        public void PresentAim(AimInfo aimInfo)
        {
            AimView.UpdateAim(aimInfo.AimVector);
        }
        
        private IAimView AimView { get; }
    }
}