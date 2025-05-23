using Adapter.IView.InGame.Player;
using UnityEngine;

namespace Adapter.View.InGame.Player
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class AimView : MonoBehaviour, IAimView
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Vector2 baseScale;
        private Transform _modelTransform;

        private void Awake()
        {
            _modelTransform = transform;
        }

        public void ShowAim()
        {
            spriteRenderer.enabled = true;
        }

        public void HideAim()
        {
            spriteRenderer.enabled = false;
        }

        public void UpdateAim(Vector2 direction)
        {
            // 回転：ベクトルの角度をZ軸に反映
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            _modelTransform.rotation = Quaternion.Euler(0f, 0f, angle);

            // スケール変更：方向ベクトルの長さに応じて矢印を伸ばす
            var length = direction.magnitude;
            _modelTransform.localScale = new Vector3(baseScale.x * length, baseScale.y);
            spriteRenderer.color = Color.Lerp(Color.green, Color.red, length);
        }
    }
}