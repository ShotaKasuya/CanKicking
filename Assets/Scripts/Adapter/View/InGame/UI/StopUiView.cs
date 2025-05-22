using Adapter.IView.InGame.UI;
using UnityEngine;

namespace Adapter.View.InGame.UI
{
    public class StopUiView : MonoBehaviour, IStopUiView
    {
        private GameObject _gameObject;

        private void Awake()
        {
            _gameObject = gameObject;
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