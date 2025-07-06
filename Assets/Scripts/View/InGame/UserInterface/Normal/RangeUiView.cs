using Interface.InGame.UserInterface;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    [RequireComponent(typeof(Image))]
    public class RangeUiView : MonoBehaviour, IRangeView
    {
        private RectTransform _parentTransform;
        private RectTransform _selfTransform;
        private Image _image;
        private Camera _camera;

        private void Awake()
        {
            _selfTransform = GetComponent<RectTransform>()!;
            _parentTransform = _selfTransform.parent as RectTransform;
            _image = GetComponent<Image>()!;
            _camera = Camera.main;

            Assert.NotNull(_selfTransform);
            Assert.NotNull(_parentTransform);
            Assert.NotNull(_image);
            Assert.NotNull(_camera);
        }

        public void Set(Vector2 point, float radius)
        {
            _selfTransform.sizeDelta = new Vector2(radius, radius);
            _image.enabled = true;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _parentTransform, point, _camera, out var localPoint
                ))
            {
                _selfTransform.anchoredPosition = localPoint;
            }
        }

        public void Hide()
        {
            _image.enabled = false;
        }
    }
}