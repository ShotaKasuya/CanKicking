using Interface.InGame.Stage;
using Unity.Cinemachine;
using UnityEngine;

namespace View.InGame.Stage
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraView : MonoBehaviour, ICameraView
    {
        private CinemachineCamera _cinemachineCamera;

        private void Awake()
        {
            _cinemachineCamera = GetComponent<CinemachineCamera>();
        }

        public void SetOrthoSize(float orthoSize)
        {
            _cinemachineCamera.Lens.OrthographicSize = orthoSize;
        }

        public void Init(Transform playerTransform)
        {
            _cinemachineCamera.Target.TrackingTarget = playerTransform;
            _cinemachineCamera.PreviousStateIsValid = false;
        }
    }
}