using Interface.InGame.UserInterface;
using ModuleExtension.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace View.InGame.UserInterface.Goal
{
    public class GoalUiView : MonoBehaviour, IGoalUiView, IRegisterable
    {
        [SerializeField] private RestartButtonView restartButtonView;
        [SerializeField] private StageSelectButtonView stageSelectButtonView;

        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(this).AsImplementedInterfaces();
                componentsBuilder.AddInstance(restartButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(stageSelectButtonView).AsImplementedInterfaces();
            });
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }
    }
}