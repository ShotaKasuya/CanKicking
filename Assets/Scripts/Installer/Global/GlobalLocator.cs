using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Model.Global;
using Module.SceneReference;
using TNRD;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using View.Global.Input;

namespace Installer.Global
{
    public class GlobalLocator : LifetimeScope
    {
        [SerializeField] private SceneReference uiScene;
        [SerializeField] private SerializableInterface<ISceneLoaderView> sceneLoaderView;
        [SerializeField] private SerializableInterface<ITimeScaleModel> timeScaleModel;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<InputSystem_Actions>(Lifetime.Singleton);

            builder.Register<ScreenScaleModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.UseComponents(componentsBuilder => { componentsBuilder.AddInstance(sceneLoaderView.Value); });

            builder.RegisterInstance(timeScaleModel.Value);
        }
    }
}