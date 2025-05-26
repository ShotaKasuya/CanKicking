using System;
using Adapter.IView.OutGame.Title;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.OutGame.Title
{
    [RequireComponent(typeof(Button))]
    public class StartButtonView: MonoBehaviour, IStartGameView, IDisposable
    {
        private void Awake()
        {
            _subject = new Subject<Unit>();
            _startButton = GetComponent<Button>();
            _startButton.onClick.AddListener(() => _subject.OnNext(Unit.Default));
        }

        public Observable<Unit> StartEvent => _subject;

        private Subject<Unit> _subject;
        private Button _startButton;

        public void Dispose()
        {
            _startButton.onClick.RemoveAllListeners();
        }
    }
}