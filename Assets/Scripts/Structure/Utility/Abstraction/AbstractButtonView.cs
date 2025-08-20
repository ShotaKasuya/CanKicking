using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Structure.Utility.Abstraction
{
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView<T> : MonoBehaviour
    {
        protected Subject<T> ButtonSubject { get; } = new Subject<T>();
        protected abstract T EventValue { get; }

        private void Awake()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnClick()
        {
            ButtonSubject.OnNext(EventValue);
        }
    }
}