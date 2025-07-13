using Controller.Global.UserInterface;
using Interface.Global.UserInterface;
using TNRD;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.Global
{
    public class GlobalUiInstaller: LifetimeScope
    {
        [SerializeField] private SerializableInterface<ITouchPositionUiView> touchPositionUiView;
        
        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(touchPositionUiView.Value);
            });
            
            builder.UseEntryPoints(pointsBuilder =>
            {
                pointsBuilder.Add<GlobalUiController>();
            });
        }
    }
}