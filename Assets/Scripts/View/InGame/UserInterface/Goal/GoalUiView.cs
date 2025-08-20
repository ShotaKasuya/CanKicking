using Cysharp.Threading.Tasks;
using Interface.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using ModuleExtension.VContainer;
using UnityEngine;
using VContainer;

namespace View.InGame.UserInterface.Goal
{
    public class GoalUiView : MonoBehaviour, IGoalUiView, IRegisterable
    {
        [SerializeField] private FadeContainer fadeContainer;

        public void Register(IContainerBuilder builder)
        {
            var fadeTargets = fadeContainer.Targets;
            builder.RegisterInstance(this).AsImplementedInterfaces();
            foreach (var (targetType, targetTransform)  in fadeTargets)
            {
                builder.RegisterInstance(targetTransform.GetComponent(targetType)).AsImplementedInterfaces();
            }
        }

        public async UniTask Show()
        {
            gameObject.SetActive(true);
            await fadeContainer.FadeIn();
        }

        public async UniTask Hide()
        {
            await fadeContainer.FadeOut();
            gameObject.SetActive(false);
        }
    }
}