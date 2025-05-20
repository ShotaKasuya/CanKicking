using Adapter.Presenter.OutGame.StageSelect;
using Adapter.Repository.OutGame;
using Adapter.View.OutGame.StageSelect;
using Domain.IUseCase.OutGame;
using Domain.UseCase.OutGame.StageSelect;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame
{
    public class StageSelectInstaller: LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterEntryPoint<SelectInputView>();
            
            // Presenter
            builder.Register<SelectedStagePresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Repository
            builder.Register<SelectedStageRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // UseCase
            builder.Register<StageSelectState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<StageSelectStateMachine>();
            builder.Register<SelectNoneCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SelectSomeCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}