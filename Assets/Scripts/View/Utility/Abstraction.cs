using Module.EditorExtension.Runtime;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace View.Utility
{
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView<T> : MonoBehaviour
    {
        protected Subject<T> ButtonSubject { get; } = new Subject<T>();
        protected abstract T EventValue { get; }
        protected Button InnerButton => innerButton;

        [SerializeField, AutoAssign] private Button innerButton;

        private void Awake()
        {
            InnerButton.onClick.AddListener(OnClick);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnClick()
        {
            ButtonSubject.OnNext(EventValue);
        }

        public void EnableInteraction()
        {
            InnerButton!.interactable = true;
        }

        public void DisableInteraction()
        {
            InnerButton!.interactable = false;
        }
    }
}