using Domain.IPresenter.InGame.Player;
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
            IPlayerPresenter playerPresenter,
            ISpawnPositionPresenter spawnPositionPresenter
        )
        {
            HeightUiPresenter = heightUiPresenter;
            PlayerPresenter = playerPresenter;
            SpawnPositionPresenter = spawnPositionPresenter;
        }

        public void Tick()
        {
            var height = PlayerPresenter.Position.y - SpawnPositionPresenter.SpawnPosition.y;
            HeightUiPresenter.SetHeight(height);
        }

        private IHeightUiPresenter HeightUiPresenter { get; }
        private IPlayerPresenter PlayerPresenter { get; }
        private ISpawnPositionPresenter SpawnPositionPresenter { get; }
    }
}