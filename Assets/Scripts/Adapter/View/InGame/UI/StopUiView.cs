using Adapter.IView.InGame.UI;
using Cysharp.Threading.Tasks;
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