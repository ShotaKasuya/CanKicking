using Adapter.IView.InGame.Ui;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.Ui.Stop
{
    [RequireComponent(typeof(Button))]
    public class PlayButtonView : MonoBehaviour, IPlayButtonView
    {
        private Subject<Unit> _subject;
        public Observable<Unit> Performed => _subject;

        private void Awake()
        {
            _subject = new Subject<Unit>();
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            _subject.OnNext(Unit.Default);
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