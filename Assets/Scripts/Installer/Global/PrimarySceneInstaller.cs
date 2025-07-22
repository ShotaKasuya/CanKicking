using Controller.Global.Scene;
using Interface.Global.Audio;
using Interface.Global.Scene;
using TNRD;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Installer.Global
{
    /// <summary>
    /// 複数シーンをロードする際に中核となるシーンに配置する
    /// </summary>
    public class PrimarySceneInstaller : LifetimeScope
    {
        [SerializeField] private SerializableInterface<IBgmModel> bgmModel;
        [SerializeField] private SerializableInterface<IResourceScenesModel> sceneResources;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(bgmModel.Value);
                componentsBuilder.AddInstance(sceneResources.Value);
            });

            builder.RegisterEntryPoint<ResourceSceneController>();
        }
    }
}