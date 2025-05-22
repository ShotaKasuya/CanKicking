using System;
using Adapter.IView.InGame.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.UI
{
    [RequireComponent(typeof(Button))]
    public class StopButton : MonoBehaviour, IStopButtonView
    {
        public Action StopEvent { get; set; }

        private GameObject _gameObject;

        private void Awake()
        {
            _gameObject = gameObject;
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        private void Invoke()
        {
            StopEvent?.Invoke();
        }

        public void Show()
        {
            _gameObject.SetActive(true);
        }

        public void Hide()
        {
            _gameObject.SetActive(false);
        }
    }
}