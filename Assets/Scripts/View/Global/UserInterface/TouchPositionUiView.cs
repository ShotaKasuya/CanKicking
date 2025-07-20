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
        [SerializeField] private Canvas canvas;
        [SerializeField] private float fadeDuration;

        private Camera _mainCamera;
        private RectTransform _canvasTransform;
        private RectTransform _selfTransform;
        private Vector2 _defaultSizeDelta;
        private GameObject _self;

        private void Awake()
        {
            SceneManager.sceneLoaded += SetCamera;
            _canvasTransform = canvas.GetComponent<RectTransform>();
            _selfTransform = GetComponent<RectTransform>();
            _self = gameObject;
            _defaultSizeDelta = _selfTransform.sizeDelta;

            _self.SetActive(false);
        }

        private void SetCamera(UnityEngine.SceneManagement.Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= SetCamera;
            if (canvas.renderMode == RenderMode.ScreenSpaceOverlay)
            {
                return;
            }

            _mainCamera = Camera.main;
        }

        public async UniTask FadeIn(Vector2 screenPosition)
        {
            _self.SetActive(true);
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _canvasTransform,
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
            _self.SetActive(false);
        }
    }
}