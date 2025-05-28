using Adapter.IView.InGame.UI;
using R3;
using Structure.Scene;
using UnityEngine;
using UnityEngine.UI;

namespace Adapter.View.InGame.UI
{
    [RequireComponent(typeof(Button))]
    public abstract class SceneChangeButton : MonoBehaviour, ISceneChangeEventView
    {
        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(Invoke);
            Subject = new Subject<SceneType>();
        }

        protected virtual void OnAwake()
        {
        }

        protected abstract void Invoke();

        protected Subject<SceneType> Subject;
        public Observable<SceneType> Performed => Subject;
    }
}