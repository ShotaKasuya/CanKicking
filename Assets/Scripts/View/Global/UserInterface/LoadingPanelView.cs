using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.View.Global;
using LitMotion;
using LitMotion.Extensions;
using Structure.Utility.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace View.Global.UserInterface
{
    [RequireComponent(typeof(Image))]
    public class LoadingPanelView : MonoBehaviour, ILoadingPanelView
    {
        private const float Empty = 0f;
        private const float Filled = 1f;

        private Image _panel;
        [SerializeField] private float fadeDuration;

        private void Awake()
        {
            _panel = GetComponent<Image>();
        }

        public async UniTask ShowPanel(CancellationToken cancellationToken = new CancellationToken())
        {
            var current = _panel.fillAmount;
            _panel.enabled = true;
            _panel.SetHorizontal(Extension.HorizontalOrigin.Left);
            await LMotion.Create(current, Filled, fadeDuration)
                .BindToFillAmount(_panel)
                .ToUniTask(cancellationToken);
        }

        public async UniTask HidePanel(CancellationToken cancellationToken = new CancellationToken())
        {
            var current = _panel.fillAmount;
            _panel.SetHorizontal(Extension.HorizontalOrigin.Right);
            await LMotion.Create(current, Empty, fadeDuration)
                .BindToFillAmount(_panel)
                .ToUniTask(cancellationToken);
            _panel.enabled = false;
        }
    }
}