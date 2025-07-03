using Interface.InGame.Player;
using UnityEngine;

namespace View.InGame.Player
{
    public class AimView : MonoBehaviour, IAimView
    {
        [SerializeField] private Vector2 baseScale;
        [SerializeField] private int circleSegments;
        [SerializeField] private LineRenderer? validRangeCircle;
        [SerializeField] private LineRenderer? cancelRangeCircle;

        private Transform? _modelTransform;
        private SpriteRenderer? _spriteRenderer;

        private void Awake()
        {
            _modelTransform = transform;
            _spriteRenderer = GetComponent<SpriteRenderer>();
            validRangeCircle!.transform.parent = null;
            cancelRangeCircle!.transform.parent = null;
        }

        public void SetAim(AimContext aimContext)
        {
            InnerAim(aimContext.AimVector);
            DrawCircle(validRangeCircle!, aimContext.StartPoint, aimContext.CancelRadius, Color.green);
            DrawCircle(cancelRangeCircle!, aimContext.StartPoint, aimContext.CancelRadius + aimContext.MaxRadius,
                Color.red);
        }

        public void Show(Vector2 startPoint)
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

        private void DrawCircle(LineRenderer lineRenderer, Vector2 center, float radius, Color color)
        {
            var segments = circleSegments;
            lineRenderer.positionCount = segments + 1;
            lineRenderer.startColor = lineRenderer.endColor = color;

            for (int i = 0; i <= segments; i++)
            {
                float angle = i * 2f * Mathf.PI / segments;
                Vector2 pos = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
                lineRenderer.SetPosition(i, pos);
            }
        }
    }
}