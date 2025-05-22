using System;
using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class PlayEventPresenter : IPlayEventPresenter, IDisposable
    {
        public PlayEventPresenter
        (
            IPlayButtonView playButtonView
        )
        {
            PlayButtonView = playButtonView;

            PlayButtonView.PlayEvent += Invoke;
        }

        public Action PlayEvent { get; set; }

        private void Invoke()
        {
            PlayEvent.Invoke();
        }

        private IPlayButtonView PlayButtonView { get; }

        public void Dispose()
        {
            PlayButtonView.PlayEvent -= Invoke;
        }
    }
}