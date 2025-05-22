using System;

namespace Adapter.IView.InGame.UI
{
    public interface INormalUiView
    {
        public void Show();
        public void Hide();
    }

    public interface IStopButtonView
    {
        public Action StopEvent { get; set; }
    }
}