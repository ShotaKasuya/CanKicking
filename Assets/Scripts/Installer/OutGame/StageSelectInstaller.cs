using Adapter.Presenter.OutGame.StageSelect;
using Adapter.Repository.OutGame;
using Adapter.View.OutGame.StageSelect;
using Domain.IUseCase.OutGame;
using Domain.UseCase.OutGame.StageSelect;
using Module.StateMachine;
using Structure.OutGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame
{
    public class StageSelectInstaller : LifetimeScope
    {
        [SerializeField] private SelectionTextView selectionTextView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.RegisterEntryPoint<SelectInputView>();
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(selectionTextView).AsImplementedInterfaces();
            });

            // Presenter
            builder.Register<PlayerStageSelectionPresenter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<StageSelectPresenter>(Lifetime.Singleton).AsImplementedInterfaces();

            // Repository
            builder.Register<SelectedStageRepository>(Lifetime.Singleton).AsImplementedInterfaces();

            // UseCase
            builder.Register<StageSelectState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<StageSelectStateMachine>();
            builder.Register<SelectNoneCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SelectSomeCase>(Lifetime.Singleton).AsImplementedInterfaces();
        }

        private void OnGUI()
        {
            var state = Container.Resolve<IMutStateEntity<StageSelectStateType>>();
            var style = new GUIStyle()
            {
                fontSize = 130
            };
            GUI.Label(new Rect(10, 10, 100, 20), state.State.ToString(), style);
        }
    }
}