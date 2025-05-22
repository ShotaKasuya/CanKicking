using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class NormalUiPresenter: INormalUiPresenter
    {
        public NormalUiPresenter
        (
            INormalUiView normalUiView
        )
        {
            NormalUiView = normalUiView;
        }
        
        public void ShowUi()
        {
            NormalUiView.Show();
        }

        public void HideUi()
        {
            NormalUiView.Hide();
        }
        
        private INormalUiView NormalUiView { get; }
    }
}