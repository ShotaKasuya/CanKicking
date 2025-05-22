using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class StopUiPresenter: IStopUiPresenter
    {
        public StopUiPresenter
        (
            IStopUiView stopUiView
        )
        {
            StopUiView = stopUiView;
        }
        
        public void ShowUi()
        {
            StopUiView.Show();
        }

        public void HideUi()
        {
            StopUiView.Hide();
        }
        
        private IStopUiView StopUiView { get; }
    }
}