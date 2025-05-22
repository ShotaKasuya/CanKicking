using System;

namespace Adapter.IView.InGame.UI
{
    public interface IStopUiView
    {
        public void Show();
        public void Hide();
    }

    public interface IPlayButtonView
    {
        public Action PlayEvent { get; set; }
    }
}