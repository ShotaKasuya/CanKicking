using UnityEngine;

namespace Adapter.IView.InGame.Player
{
    public interface IAimView
    {
        public void ShowAim();
        public void HideAim();
        public void UpdateAim(Vector2 direction);
    }
}