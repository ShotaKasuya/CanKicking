using Controller.OutGame.Title;
using Cysharp.Threading.Tasks;
using Model.OutGame.Title;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.OutGame.Title
{
    public class TitleInstaller : LifetimeScope
    {
        [SerializeField] private GameStartSceneModel gameStartSceneModel;

        protected override void Configure(IContainerBuilder builder)
        {
            // Model
            builder.RegisterInstance(gameStartSceneModel).AsImplementedInterfaces();

            // Controller
            builder.RegisterEntryPoint<TitleController>();
        }

        private void Start()
        {
            UniTask.RunOnThreadPool((o =>
            {
                var lifetimeScope = o as LifetimeScope;
                lifetimeScope!.Build();
            }), this).Forget();
        }
    }
}