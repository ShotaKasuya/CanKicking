using Interface.OutGame.StageSelect;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    /// <summary>
    /// `Cinemachine`の`Target`となるView
    /// </summary>
    public class CameraPointView : MonoBehaviour, ICameraPositionView
    {
        [SerializeField] private Transform minTransform;
        [SerializeField] private Transform maxTransform;
        [SerializeField] private float sensitivity = 10f;
        [SerializeField] private float damping = 5f;

        private Vector2 _velocity;
        private Transform _modelTransform;

        private void Awake()
        {
            _modelTransform = transform;
            minTransform.parent = null;
            maxTransform.parent = null;
        }

        public void AddForce(Vector2 vector2)
        {
            _velocity += vector2 * sensitivity;
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            var position = (Vector2)_modelTransform.position;

            // 位置更新
            var newPosition = position + _velocity * dt;

            // 範囲制限
            var clampedX = Mathf.Clamp(newPosition.x, minTransform.position.x, maxTransform.position.x);
            var clampedY = Mathf.Clamp(newPosition.y, minTransform.position.y, maxTransform.position.y);
            _modelTransform.position = new Vector3(clampedX, clampedY);

            // 減衰処理
            _velocity = Vector2.Lerp(_velocity, Vector2.zero, damping * dt);
        }
    }
}