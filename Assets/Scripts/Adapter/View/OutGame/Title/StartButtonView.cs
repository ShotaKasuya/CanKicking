using System;
using Adapter.IView.OutGame.Title;
using UnityEngine;
using UnityEngine.UI;

namespace Detail.View.OutGame.Title
{
    [RequireComponent(typeof(Button))]
    public class StartButtonView: MonoBehaviour, IStartGameView, IDisposable
    {
        private void Awake()
        {
            _startButton = GetComponent<Button>();
            _startButton.onClick.AddListener(() => StartEvent.Invoke());
        }

        public Action StartEvent { get; set; }

        private Button _startButton;

        public void Dispose()
        {
            _startButton.onClick.RemoveAllListeners();
        }
    }
}