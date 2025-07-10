using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Interface.Global.UserInterface;

public interface ITouchPositionUiView
{
    public UniTask FadeIn(Vector2 screenPosition);
    public UniTask FadeOut();
}