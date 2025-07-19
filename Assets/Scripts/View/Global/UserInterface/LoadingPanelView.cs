using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface.Global.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace View.Global.UserInterface
{
    [RequireComponent(typeof(Image))]
    public class LoadingPanelView:MonoBehaviour,ILoadingPanelView
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
            await _panel.DOFillAmount(Filled, fadeDuration)
                .AsyncWaitForCompletion().AsUniTask();
        }

        public async UniTask HidePanel()
        {
            await _panel.DOFillAmount(Empty, fadeDuration)
                .AsyncWaitForCompletion().AsUniTask();
            _panel.enabled = false;
        }
    }
}