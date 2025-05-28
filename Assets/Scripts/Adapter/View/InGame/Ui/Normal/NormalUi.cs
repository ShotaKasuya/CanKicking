using Adapter.IView.InGame.Ui;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Adapter.View.InGame.Ui.Normal
{
    public class NormalUi: MonoBehaviour, IRegisterable, INormalUiView
    {
        [SerializeField] private HeightView heightView;
        [SerializeField] private StopButton stopButton;

        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(this).AsImplementedInterfaces();
                componentsBuilder.AddInstance(heightView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(stopButton).AsImplementedInterfaces();
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