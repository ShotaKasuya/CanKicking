using Interface.View.InGame.UserInterface;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    [RequireComponent(typeof(Slider))]
    public class ProgressUiView : MonoBehaviour, IProgressUiView
    {
        private Slider _slider;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        public void SetProgress(float progress)
        {
            _slider.value = progress;
        }
    }
}