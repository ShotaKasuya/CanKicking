using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.View.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using Structure.Utility.Extension;
using TNRD;
using UnityEngine;
using VContainer;

namespace View.InGame.UserInterface.Goal
{
    public class GoalUiView : MonoBehaviour, IGoalUiView, IRegisterable
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private SerializableInterface<IGoal_RestartButtonView> restartButtonView;
        [SerializeField] private SerializableInterface<IGoal_StageSelectButtonView> stageSelectButtonView;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(this).AsImplementedInterfaces();
            builder.RegisterInstance(restartButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(stageSelectButtonView).AsImplementedInterfaces();
        }

        public async UniTask Show(CancellationToken token)
        {
            gameObject.SetActive(true);
            await fadeContainer.FadeIn(token);
            restartButtonView.Value.EnableInteraction();
            stageSelectButtonView.Value.EnableInteraction();
        }

        public async UniTask Hide(CancellationToken token)
        {
            restartButtonView.Value.DisableInteraction();
            stageSelectButtonView.Value.DisableInteraction();
            await fadeContainer.FadeOut(token);
            gameObject.SetActive(false);
        }
    }
}