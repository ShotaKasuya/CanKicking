using Controller.OutGame.StageSelect;
using Controller.OutGame.StageSelect.UserInterface;
using Model.OutGame.StageSelect;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.OutGame.StageSelect;

namespace Installer.OutGame.StageSelect
{
    public class StageSelectInstaller : LifetimeScope
    {
        [SerializeField] private SelectedStageView selectedStageView;
        [SerializeField] private StagesBind stagesBind;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.Register<SelectionView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(selectedStageView).AsImplementedInterfaces();

            // Model
            stagesBind.Register(builder);
            builder.Register<SelectedStageModel>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<InitializeController>();
            builder.Register<StageSelectState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<StageSelectStateMachine>();
            builder.Register<NoneStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SomeStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}