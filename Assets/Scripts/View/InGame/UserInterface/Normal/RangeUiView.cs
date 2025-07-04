using Interface.InGame.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    [RequireComponent(typeof(Image))]
    public class RangeUiView : MonoBehaviour, IRangeView
    {
        private RectTransform _transform;
        private Image _image;
        private Camera _camera;

        private void Awake()
        {
            _transform = GetComponent<RectTransform>()!;
            _image = GetComponent<Image>()!;
            _camera = Camera.main!;
        }

        public void Set(Vector2 point, float radius)
        {
            _transform.sizeDelta = new Vector2(radius, radius);
            _image.enabled = true;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _transform, point, _camera, out var localPoint
                ))
            {
                // Debug.Log($"point: {point}, local point: {localPoint}");
                _transform.anchoredPosition = localPoint;
            }
            else
            {
                Debug.LogError("happen");
            }
        }

        public void Hide()
        {
            _image.enabled = false;
        }
    }
}