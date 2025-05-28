using Adapter.IView.InGame.Ui;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.Ui.Normal
{
    [RequireComponent(typeof(Button))]
    public class StopButton : MonoBehaviour, IStopEventView
    {
        private Subject<Unit> _subject;

        public Observable<Unit> OnPerformed => _subject;

        private void Awake()
        {
            _subject = new Subject<Unit>();
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            _subject.OnNext(Unit.Default);
        }

        public UniTask Show()
        {
            gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}