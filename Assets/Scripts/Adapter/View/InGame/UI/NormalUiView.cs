using Adapter.IView.InGame.UI;
using UnityEngine;

namespace Adapter.View.InGame.UI
{
    public class NormalUiView: MonoBehaviour, INormalUiView
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}