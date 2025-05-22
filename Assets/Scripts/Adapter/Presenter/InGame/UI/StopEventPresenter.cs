using System;
using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class StopEventPresenter : IStopEventPresenter, IDisposable
    {
        public StopEventPresenter
        (
            IStopButtonView stopButtonView
        )
        {
            StopButtonView = stopButtonView;

            StopButtonView.StopEvent += Invoke;
        }

        public Action StopEvent { get; set; }

        private void Invoke()
        {
            StopEvent.Invoke();
        }

        private IStopButtonView StopButtonView { get; }

        public void Dispose()
        {
            StopButtonView.StopEvent -= Invoke;
        }
    }
}