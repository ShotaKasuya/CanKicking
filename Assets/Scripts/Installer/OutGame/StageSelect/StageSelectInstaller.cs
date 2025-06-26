using Controller.OutGame.StageSelect.UserInterface;
using Model.OutGame.StageSelect;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.OutGame.StageSelect;

namespace Installer.OutGame.StageSelect
{
    public class StageSelectInstaller: LifetimeScope
    {
        [SerializeField] private SelectedStageView selectedStageView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.Register<SelectionView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(selectedStageView).AsImplementedInterfaces();
            
            // Model
            builder.Register<SelectedStageModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Controller
            builder.Register<StageSelectState>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<StageSelectStateMachine>();
            builder.Register<NoneStateController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SomeStateController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}