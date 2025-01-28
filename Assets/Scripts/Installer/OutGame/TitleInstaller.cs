using Adapter.Presenter.OutGame.Title;
using Detail.View.OutGame.Title;
using Domain.IPresenter.Scene;
using Domain.UseCase.OutGame.Title;
using UnityEngine;

namespace Installer.OutGame
{
    public class TitleInstaller: InstallerBase
    {
        [SerializeField] private StartButtonView startButtonView;
        
        protected override void CustomConfigure()
        {
            // Presenter
            var scenePresenter = GlobalLocator.Resolve<IScenePresenter>();
            var titleEventPresenter = new TitleEventPresenter(startButtonView);
            
            // UseCase
            var titleCase = new TitleCase(titleEventPresenter, scenePresenter);
            RegisterEntryPoints(titleCase);
        }
    }
}