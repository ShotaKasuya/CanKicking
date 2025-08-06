using Cysharp.Threading.Tasks;
using Interface.InGame.UserInterface;
using UnityEngine;
using VContainer;
using VContainer.ModuleExtension;
using VContainer.Unity;

namespace View.InGame.UserInterface.Normal
{
    public class NormalUiView: MonoBehaviour, INormalUiView, IRegisterable
    {
        [SerializeField] private HeightUiView heightUiView;
        [SerializeField] private ProgressUiView progressUiView;
        [SerializeField] private StopButtonView stopButtonView;
        
        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(this).AsImplementedInterfaces();
                componentsBuilder.AddInstance(heightUiView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(progressUiView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(stopButtonView).AsImplementedInterfaces();
            });
        }
        
        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}