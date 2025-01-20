using Adapter.Presenter.InGame.Stage;
using Detail.View.InGame.Stage;
using Domain.IPresenter.InGame.Stage;
using UnityEngine;

namespace Installer.InGame
{
    public class StageInstaller: InstallerBase
    {
        [SerializeField] private SpawnPositionView spawnPositionView;

        protected override void CustomConfigure()
        {
            // Presenter
            var spawnPositionPresenter = new SpawnPositionPresenter(spawnPositionView);
            RegisterInstance<ISpawnPositionPresenter, SpawnPositionPresenter>(spawnPositionPresenter);
        }
    }
}