using Interface.InGame.UserInterface;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    public class StopButtonView : MonoBehaviour, IStopButtonView
    {
        public Observable<Unit> Performed => Subject;

        private Subject<Unit> Subject { get; } = new Subject<Unit>();
        [SerializeField] private Button stopButton;

        private void Awake()
        {
            stopButton.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            Subject.OnNext(Unit.Default);
        }
    }
}