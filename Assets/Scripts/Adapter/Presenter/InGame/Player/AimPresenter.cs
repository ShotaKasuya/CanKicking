using Adapter.IView.InGame.Player;
using Domain.IPresenter.InGame.Player;
using UnityEngine;

namespace Adapter.Presenter.InGame.Player
{
    public class AimPresenter : IAimPresenter
    {
        public AimPresenter
        (
            IAimView aimView
        )
        {
            _isShowed = false;
            AimView = aimView;

            AimView.HideAim();
        }

        public void PresentAim(AimInfo aimInfo)
        {
            if (aimInfo.AimVector == Vector2.zero)
            {
                AimView.HideAim();
                _isShowed = false;
                return;
            }

            AimView.UpdateAim(aimInfo.AimVector);
            if (!_isShowed)
            {
                _isShowed = true;
                AimView.ShowAim();
            }
        }

        private bool _isShowed;
        private IAimView AimView { get; }
    }
}