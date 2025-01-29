using Adapter.Presenter.Util;
using Detail.View.View;
using Domain.IPresenter.Util.Input;
using Domain.UseCase.Util;
using UnityEngine;

namespace Installer.OutGame
{
    public class StageSelectInstaller: InstallerBase
    {
        [SerializeField] private MovableView movableView;
        
        protected override void CustomConfigure()
        {
            // Presenter
            var fingerTouchingEventPresenter = GlobalLocator.Resolve<IFingerTouchingEventPresenter>();
            var scrollPresenter = new ScrollPresenter(movableView);
            RegisterEntryPoints(scrollPresenter);
            
            // UseCase
            var scrollCase = new ScrollCase(fingerTouchingEventPresenter, scrollPresenter);
            RegisterEntryPoints(scrollCase);
        }
    }
}