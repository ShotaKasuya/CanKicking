using Domain.IPresenter.InGame.Stage;
using Domain.IPresenter.InGame.UI;
using VContainer.Unity;

namespace Domain.UseCase.InGame.UI
{
    /// <summary>
    /// 一番下から何mか表示する
    /// </summary>
    public class ShowHeightCase : ITickable
    {
        public ShowHeightCase
        (
            IHeightUiPresenter heightUiPresenter,
            IPlayerHeightPresenter playerHeightPresenter
        )
        {
            HeightUiPresenter = heightUiPresenter;
            PlayerHeightPresenter = playerHeightPresenter;
        }

        public void Tick()
        {
            var height = PlayerHeightPresenter.GetHeight();
            HeightUiPresenter.SetHeight(height);
        }

        private IHeightUiPresenter HeightUiPresenter { get; }
        private IPlayerHeightPresenter PlayerHeightPresenter { get; }
    }
}