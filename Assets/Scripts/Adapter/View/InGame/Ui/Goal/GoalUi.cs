using Adapter.IView.InGame.Ui;
using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Adapter.View.InGame.Ui.Goal
{
    public class GoalUi: MonoBehaviour, IRegisterable, IGoalUiView
    {
        [SerializeField] private ReStartButtonView reStartButtonView;
        [SerializeField] private ToStageSelectButtonView toStageSelectButtonView;
        
        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(this).AsImplementedInterfaces();
                componentsBuilder.AddInstance(reStartButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(toStageSelectButtonView).AsImplementedInterfaces();
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