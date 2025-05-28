using Adapter.Presenter.OutGame.StageSelect;
using Adapter.Repository.OutGame;
using Adapter.View.OutGame.StageSelect;
using Domain.Controller.OutGame.StageSelect;
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
            builder.Register<StageSelectPresenter>(Lifetime.Scoped).AsImplementedInterfaces();

            // Repository
            builder.Register<StageSelectStateRepository>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SelectedStageRepository>(Lifetime.Scoped).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<StageSelectStateMachine>();
            builder.Register<NoneStateController>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<SomeStateController>(Lifetime.Scoped).AsImplementedInterfaces();
        }

#if UNITY_EDITOR
        private IMutStateEntity<StageSelectStateType> _state;

        private void Start()
        {
            _state = Container.Resolve<IMutStateEntity<StageSelectStateType>>();
        }

        private void OnGUI()
        {
            var style = new GUIStyle()
            {
                fontSize = 130
            };
            GUI.Label(new Rect(10, 10, 100, 20), _state.State.ToString(), style);
        }
#endif
    }
}