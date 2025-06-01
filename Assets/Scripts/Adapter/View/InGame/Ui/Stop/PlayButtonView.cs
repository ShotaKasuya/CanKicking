using Adapter.IView.InGame.Ui;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.Ui.Stop
{
    [RequireComponent(typeof(Button))]
    public class PlayButtonView : MonoBehaviour, IPlayButtonView
    {
        private Subject<Unit> Subject { get; } = new Subject<Unit>();
        public Observable<Unit> Performed => Subject;

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            Subject.OnNext(Unit.Default);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

    }
}