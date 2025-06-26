using Cysharp.Threading.Tasks;
using Interface.InGame.UserInterface;
using Structure.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace View.InGame.UserInterface.Stop
{
    public class StopUiView : MonoBehaviour, IStopUiView, IRegisterable
    {
        [SerializeField] private PlayButtonView playButtonView;
        [SerializeField] private RestartButtonView restartButtonView;
        [SerializeField] private StageSelectButtonView stageSelectButtonView;

        public void Register(IContainerBuilder builder)
        {
            builder.UseComponents(componentsBuilder =>
            {
                componentsBuilder.AddInstance(this).AsImplementedInterfaces();
                componentsBuilder.AddInstance(playButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(restartButtonView).AsImplementedInterfaces();
                componentsBuilder.AddInstance(stageSelectButtonView).AsImplementedInterfaces();
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