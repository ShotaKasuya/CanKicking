using Cysharp.Threading.Tasks;
using DG.Tweening;
using Interface.Global.UserInterface;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace View.Global.UserInterface
{
    [RequireComponent(typeof(Image))]
    public class TouchPositionUiView : MonoBehaviour, ITouchPositionUiView
    {
        [SerializeField] private RectTransform canvasTransform;
        [SerializeField] private float fadeDuration;
        private Camera _mainCamera;
        private RectTransform _selfTransform;
        private Vector2 _defaultSizeDelta;
        private Image _selfImage;

        private void Awake()
        {
            SceneManager.sceneLoaded += SetCamera;
            _selfTransform = GetComponent<RectTransform>();
            _selfImage = GetComponent<Image>();
            _defaultSizeDelta = _selfTransform.sizeDelta;
        }

        private void SetCamera(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            _mainCamera = Camera.main;
        }

        public async UniTask FadeIn(Vector2 screenPosition)
        {
            _selfImage.enabled = true;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    canvasTransform,
                    screenPosition,
                    _mainCamera,
                    out var localPos))
            {
                Debug.LogWarning("ScreenPointToLocalPointInRectangle変換失敗");
            }

            
            _selfTransform.anchoredPosition = localPos;
            await _selfTransform.DOSizeDelta(_defaultSizeDelta, fadeDuration).AsyncWaitForCompletion().AsUniTask();
        }

        public async UniTask FadeOut()
        {
            await _selfTransform.DOSizeDelta(Vector2.zero, fadeDuration).AsyncWaitForCompletion().AsUniTask();
            _selfImage.enabled = false;
        }
    }
}