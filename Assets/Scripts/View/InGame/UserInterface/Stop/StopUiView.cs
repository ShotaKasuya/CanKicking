using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.View.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using Structure.Utility.Extension;
using TNRD;
using UnityEngine;
using VContainer;

namespace View.InGame.UserInterface.Stop
{
    public class StopUiView : MonoBehaviour, IStopUiView, IRegisterable
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private SerializableInterface<IPlayButtonView> playButtonView;
        [SerializeField] private SerializableInterface<IStop_RestartButtonView> restartButtonView;
        [SerializeField] private SerializableInterface<IStop_StageSelectButtonView> stageSelectButtonView;

        public void Register(IContainerBuilder builder)
        {
            Debug.Log("VAR");
            builder.RegisterInstance(this).AsImplementedInterfaces();
            builder.RegisterInstance(playButtonView.Value).AsImplementedInterfaces();
            builder.RegisterInstance(restartButtonView.Value).AsImplementedInterfaces();
            builder.RegisterInstance(stageSelectButtonView.Value).AsImplementedInterfaces();
        }

        public async UniTask Show(CancellationToken token)
        {
            gameObject.SetActive(true);
            await fadeContainer.FadeIn(token);
            playButtonView.Value.EnableInteraction();
            restartButtonView.Value.EnableInteraction();
            stageSelectButtonView.Value.EnableInteraction();
        }

        public async UniTask Hide(CancellationToken token)
        {
            playButtonView.Value.DisableInteraction();
            restartButtonView.Value.DisableInteraction();
            stageSelectButtonView.Value.DisableInteraction();
            await fadeContainer.FadeOut(token);
            gameObject.SetActive(false);
        }
    }
}