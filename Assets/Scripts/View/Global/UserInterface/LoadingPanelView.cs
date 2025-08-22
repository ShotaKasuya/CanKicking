using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface.View.Global;
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

        public async UniTask ShowPanel()
        {
            _panel.enabled = true;
            _panel.SetHorizontal(Extension.HorizontalOrigin.Left);
            await _panel.DOFillAmount(Filled, fadeDuration)
                .AsyncWaitForCompletion().AsUniTask();
        }

        public async UniTask HidePanel()
        {
            _panel.SetHorizontal(Extension.HorizontalOrigin.Right);
            await _panel.DOFillAmount(Empty, fadeDuration)
                .AsyncWaitForCompletion().AsUniTask();
            _panel.enabled = false;
        }
    }
}