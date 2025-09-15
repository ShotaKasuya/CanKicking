using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.View.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using Structure.Utility.Extension;
using TNRD;
using UnityEngine;
using VContainer;

namespace View.InGame.UserInterface.Normal
{
    public class NormalUiView : MonoBehaviour, INormalUiView, IRegisterable
    {
        [SerializeField] private FadeContainer fadeContainer;
        [SerializeField] private SerializableInterface<IStopButtonView> stopButtonView;
        [SerializeField] private SerializableInterface<IKickCountUiView> kickCountUiView;
        [SerializeField] private SerializableInterface<IProgressUiView> progressUiView;

        public void Register(IContainerBuilder builder)
        {
            builder.RegisterInstance(this).AsImplementedInterfaces();
            builder.RegisterInstance(stopButtonView).AsImplementedInterfaces();
            builder.RegisterInstance(kickCountUiView).AsImplementedInterfaces();
            builder.RegisterInstance(progressUiView).AsImplementedInterfaces();
        }

        public async UniTask Show(CancellationToken token)
        {
            gameObject.SetActive(true);
            await fadeContainer.FadeIn(token);
            stopButtonView.Value.EnableInteraction();
        }

        public async UniTask Hide(CancellationToken token)
        {
            stopButtonView.Value.DisableInteraction();
            await fadeContainer.FadeOut(token);
            gameObject.SetActive(false);
        }
    }
}