using Cysharp.Threading.Tasks;
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