using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.InGame.UserInterface;
using Module.FadeContainer.Runtime;
using Structure.Utility.Extension;
using UnityEngine;
using VContainer;

namespace View.InGame.UserInterface.Stop
{
    public class StopUiView : MonoBehaviour, IStopUiView, IRegisterable
    {
        [SerializeField] private FadeContainer fadeContainer;

        public void Register(IContainerBuilder builder)
        {
            var fadeTargets = fadeContainer.Targets;
            builder.RegisterInstance(this).AsImplementedInterfaces();
            foreach (var (targetType, targetTransform)  in fadeTargets)
            {
                builder.RegisterInstance(targetTransform).AsImplementedInterfaces();
            }
        }

        public UniTask Show(CancellationToken token)
        {
            gameObject.SetActive(true);
            return fadeContainer.FadeIn(token);
        }

        public async UniTask Hide(CancellationToken token)
        {
            await fadeContainer.FadeOut(token);
            gameObject.SetActive(false);
        }
    }
}