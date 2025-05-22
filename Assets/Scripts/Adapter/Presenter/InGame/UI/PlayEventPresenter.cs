using System;
using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class PlayEventPresenter : IPlayEventPresenter, IDisposable
    {
        public PlayEventPresenter
        (
            IStopButtonView stopButtonView
        )
        {
            StopButtonView = stopButtonView;

            StopButtonView.StopEvent += Invoke;
        }

        public Action PlayEvent { get; set; }

        private void Invoke()
        {
            PlayEvent.Invoke();
        }

        private IStopButtonView StopButtonView { get; }

        public void Dispose()
        {
            StopButtonView.StopEvent -= Invoke;
        }
    }
}