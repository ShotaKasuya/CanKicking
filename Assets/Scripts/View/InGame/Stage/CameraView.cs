using Interface.InGame.Stage;
using Unity.Cinemachine;
using UnityEngine;

namespace View.InGame.Stage
{
    public class CameraView : MonoBehaviour, ICameraView
    {
        [SerializeField] private CinemachineCamera cinemachine;

        public void SetOrthoSize(float orthoSize)
        {
            cinemachine.Lens.OrthographicSize = orthoSize;
        }
    }
}