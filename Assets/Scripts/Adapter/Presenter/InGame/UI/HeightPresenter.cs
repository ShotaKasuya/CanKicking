using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class HeightPresenter: IHeightUiPresenter
    {
        public HeightPresenter
        (
            IHeightView heightView
        )
        {
            HeightView = heightView;
        }
        
        public void SetHeight(float height)
        {
            HeightView.SetHeight(height);
        }
        
        private IHeightView HeightView { get; }
    }
}