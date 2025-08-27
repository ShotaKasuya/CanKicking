using System;
using Controller.Global;
using Controller.Global.UserInterface;
using GoogleMobileAds.Api;
using Logic.Global.Scene;
using Model.Global;
using Model.Global.SaveData;
using Model.Global.Scene;
using Model.Global.Utility;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.Global.Advertisement;
using View.Global.Audio;
using View.Global.Input;
using View.Global.Scene;
using View.Global.UserInterface;

namespace Installer.Global
{
    /// <summary>
    /// グローバルライフタイムスコープ
    /// </summary>
    public partial class GlobalLocator : LifetimeScope
    {
        [SerializeField] private TimeScaleModel timeScaleModel;
        [SerializeField] private TouchPositionUiView touchPositionUiView;
        [SerializeField] private LoadingPanelView loadingPanelView;
        [SerializeField] private BgmSourceView bgmSourceView;
        [SerializeField] private SeSourceView seSourceView;
        [SerializeField] private UiSourceView uiSourceView;

        protected override void Configure(IContainerBuilder builder)
        {
            // Utility
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);
            builder.Register<GlobalInputView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register(_ => new CompositeDisposable(), Lifetime.Scoped)
                .As<CompositeDisposable, IDisposable>();

            // View
            builder.Register<SceneLoaderView>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<BottomAdsView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterInstance(timeScaleModel).AsImplementedInterfaces();
            builder.RegisterInstance(touchPositionUiView).AsImplementedInterfaces();
            builder.RegisterInstance(loadingPanelView).AsImplementedInterfaces();
            builder.RegisterInstance(bgmSourceView).AsImplementedInterfaces();
            builder.RegisterInstance(seSourceView).AsImplementedInterfaces();
            builder.RegisterInstance(uiSourceView).AsImplementedInterfaces();

            // Model
            builder.Register<ScreenScaleModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SceneLoadEventModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<BlockingOperationModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PrimarySceneModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ClearRecordModel>(Lifetime.Singleton).AsImplementedInterfaces();

            // Logic
            builder.Register<LoadPrimarySceneLogic>(Lifetime.Transient).AsImplementedInterfaces();
            builder.Register<LoadResourceScenesLogic>(Lifetime.Transient).AsImplementedInterfaces();

            // Controller
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<ResourceController>();
                pointsBuilder.Add<TouchUiController>();
                pointsBuilder.Add<LoadingPanelController>();
            });
        }

        private void Start()
        {
            DebugStarter();
        }
    }
}