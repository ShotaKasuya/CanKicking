using Adapter.IView.Util.Camera;
using Unity.Cinemachine;
using UnityEngine;

namespace Detail.View.Util
{
    [RequireComponent(typeof(CinemachineCamera))]
    public class CameraView: MonoBehaviour, ICameraView
    {
        private CinemachineCamera _camera;

        private void Awake()
        {
             _camera= GetComponent<CinemachineCamera>();
        }

        public void SetOrthographicSize(float size)
        {
            _camera.Lens.OrthographicSize = size;
        }
    }
}