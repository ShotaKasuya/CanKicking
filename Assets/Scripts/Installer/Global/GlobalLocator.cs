using Controller.Global.UserInterface;
using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Interface.Global.UserInterface;
using Logic.Global.Scene;
using Model.Global;
using Model.Global.Scene;
using Model.Global.Utility;
using R3;
using TNRD;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.Global.Input;
using View.Global.Scene;

namespace Installer.Global
{
    public class GlobalLocator : LifetimeScope
    {
        [SerializeField] private SerializableInterface<ISceneLoaderView> sceneLoaderView;
        [SerializeField] private SerializableInterface<ITimeScaleModel> timeScaleModel;
        [SerializeField] private SerializableInterface<ITouchPositionUiView> touchPositionUiView;
        [SerializeField] private SerializableInterface<ILoadingPanelView> loadingPanelView;

        protected override void Configure(IContainerBuilder builder)
        {
            // Utility
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
            builder.Register<GlobalInputView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register(_ => new CompositeDisposable(), Lifetime.Scoped).AsSelf().AsImplementedInterfaces();
            
            // View
            builder.Register<NewSceneLoaderView>(Lifetime.Transient).AsImplementedInterfaces();
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(sceneLoaderView.Value);
                componentsBuilder.AddInstance(touchPositionUiView.Value);
                componentsBuilder.AddInstance(timeScaleModel.Value);
                componentsBuilder.AddInstance(loadingPanelView.Value);
            });

            // Model
            builder.Register<ScreenScaleModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoadEventModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BlockingOperationModel>(Lifetime.Singleton).AsImplementedInterfaces();
            
            // Logic
            builder.Register<LoadPrimarySceneLogic>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<LoadSceneResourcesLogic>(Lifetime.Transient).AsImplementedInterfaces();
            
            // Controller
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<TouchUiController>();
            });
        }
    }
}