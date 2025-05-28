using Adapter.IView.InGame.Ui;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Adapter.View.InGame.Ui.Stop
{
    public class StopUi: MonoBehaviour, IRegisterable, IStopUiView
    {
        [SerializeField] private PlayButtonView playButtonView;
        [SerializeField] private ToStageSelectButtonView stageSelectButtonView;
        [SerializeField] private ReStartButtonView reStartButtonView;

        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(this).AsImplementedInterfaces();
                componentsBuilder.AddInstance(playButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(stageSelectButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(reStartButtonView).AsImplementedInterfaces();
            });
        }

        public UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }
    }
}