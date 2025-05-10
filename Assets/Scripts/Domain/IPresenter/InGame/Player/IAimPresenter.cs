using UnityEngine;

namespace Domain.IPresenter.InGame.Player
{
    public interface IAimPresenter
    {
        public void PresentAim(AimInfo aimInfo);
    }

    public ref struct AimInfo
    {
        public Vector2 AimVector { get; }

        public AimInfo(Vector2 aimVector)
        {
            AimVector = aimVector;
        }
    }
}