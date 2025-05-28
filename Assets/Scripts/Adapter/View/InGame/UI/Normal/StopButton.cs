using Adapter.IView.InGame.UI;
using Cysharp.Threading.Tasks;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.UI
{
    [RequireComponent(typeof(Button))]
    public class StopButton : MonoBehaviour, INormalUiView, IStopEventView
    {
        private GameObject _gameObject;
        private Subject<Unit> _subject;

        public Observable<Unit> OnPerformed => _subject;

        private void Awake()
        {
            _gameObject = gameObject;
            _subject = new Subject<Unit>();
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            _subject.OnNext(Unit.Default);
        }

        public UniTask Show()
        {
            _gameObject.SetActive(true);
            return UniTask.CompletedTask;
        }

        public UniTask Hide()
        {
            _gameObject.SetActive(false);
            return UniTask.CompletedTask;
        }
    }
}