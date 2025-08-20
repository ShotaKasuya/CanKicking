using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Interface.Global.UserInterface;

/// <summary>
/// タッチした場所を表示する
/// </summary>
public interface ITouchPositionUiView
{
    public UniTask FadeIn(Vector2 screenPosition);
    public UniTask FadeOut();
}

/// <summary>
/// ロード中のパネル
/// </summary>
public interface ILoadingPanelView
{
    public UniTask ShowPanel();
    public UniTask HidePanel();
}

/// <summary>
/// フェードイン・アウトを行うUiのView
/// </summary>
public interface IFadeUiView
{
    public Transform SelfTransform { get; }
    public Transform FadeInPosition { get; }
    public Transform FadeOutPosition { get; }

    public async UniTask FadeIn(float fadeDuration)
    {
        await SelfTransform.DOMove(FadeInPosition.position, fadeDuration).AsyncWaitForCompletion().AsUniTask();
    }

    public async UniTask FadeOut(float fadeDuration)
    {
        await SelfTransform.DOMove(FadeOutPosition.position, fadeDuration).AsyncWaitForCompletion().AsUniTask();
    }
}