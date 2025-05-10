using Adapter.IView.Util.Camera;
using Unity.Cinemachine;
using UnityEngine;

namespace Adapter.View.Util
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraView : MonoBehaviour, ICameraView
    {
        private CinemachineCamera _camera;

        private void Awake()
        {
            _camera = GetComponent<CinemachineCamera>();
        }

        public void SetOrthographicSize(float size)
        {
            if (_camera is null)
            {
                _camera = GetComponent<CinemachineCamera>();
            }

            _camera.Lens.OrthographicSize = size;
        }
    }
}