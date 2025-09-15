using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Structure.Utility.Abstraction;

[RequireComponent(typeof(Button))]
public abstract class AbstractButtonView<T> : MonoBehaviour
{
    protected Subject<T> ButtonSubject { get; } = new Subject<T>();
    protected abstract T EventValue { get; }
    protected Button? InnerButton { get; private set; }

    private void Awake()
    {
        InnerButton = GetComponent<Button>();
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
        InnerButton!.interactable = false;
    }

    public void DisableInteraction()
    {
        InnerButton!.interactable = false;
    }
}