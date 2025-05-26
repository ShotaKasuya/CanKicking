using Adapter.View.OutGame.Title;
using Domain.Controller.OutGame;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame
{
    public class TitleInstaller: LifetimeScope
    {
        [SerializeField] private StartButtonView startButtonView;

        protected override void Configure(IContainerBuilder builder)
        {
            // View
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(startButtonView).AsImplementedInterfaces();
            });

            // Controller
            builder.RegisterEntryPoint<TitleController>();
        }
    }
}