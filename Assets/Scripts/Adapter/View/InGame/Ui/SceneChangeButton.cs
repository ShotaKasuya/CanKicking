using R3;
using Structure.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.Ui
{
    [RequireComponent(typeof(Button))]
    public abstract class SceneChangeButton : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Invoke);
        }

        protected virtual void OnAwake()
        {
        }

        protected abstract void Invoke();

        protected Subject<SceneType> Subject { get; } = new Subject<SceneType>();
        public Observable<SceneType> Performed => Subject;
    }
}