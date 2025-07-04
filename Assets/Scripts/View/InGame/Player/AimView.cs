using Interface.InGame.Player;
using UnityEngine;

namespace View.InGame.Player
{
    public class AimView : MonoBehaviour, IAimView
    {
        [SerializeField] private Vector2 baseScale;

        private Transform _modelTransform;
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _modelTransform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void SetAim(Vector2 aimVector)
        {
            InnerAim(aimVector);
        }

        public void Show()
        {
            _spriteRenderer!.enabled = true;
            InnerAim(Vector2.zero);
        }

        public void Hide()
        {
            _spriteRenderer!.enabled = false;
        }

        private void InnerAim(Vector2 aimVector)
        {
            var length = aimVector.magnitude;
            var angle = Mathf.Atan2(aimVector.y, aimVector.x) * Mathf.Rad2Deg;

            _modelTransform!.rotation = Quaternion.Euler(0f, 0f, angle);

            // スケール変更：方向ベクトルの長さに応じて矢印を伸ばす
            _modelTransform.localScale = new Vector3(baseScale.x * length, baseScale.y);
            _spriteRenderer!.color = Color.Lerp(Color.green, Color.red, length);
        }
    }
}