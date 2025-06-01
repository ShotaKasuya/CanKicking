using Adapter.IView.InGame.Ui;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.Ui.Stop
{
    [RequireComponent(typeof(Slider))]
    public class ScreenScaleSliderView: MonoBehaviour, IStopStateScreenScaleSliderView
    {
        private Slider _slider;
        private Subject<float> Subject { get; } = new Subject<float>();
        public Observable<float> ChangeObservable => Subject;

        private void Awake()
        {
            _slider = GetComponent<Slider>();
            _slider.onValueChanged.AddListener(Invoke);
        }

        private void Invoke(float value)
        {
            Subject.OnNext(value);
        }
    }
}