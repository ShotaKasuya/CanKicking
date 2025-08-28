using System.Runtime.CompilerServices;
using Module.Option.Runtime;
using R3;
using UnityEngine;

namespace Interface.View.Global;

public interface ITouchView
{
    public Observable<TouchStartEventArgument> TouchEvent { get; }
    public Option<FingerDraggingInfo> DraggingInfo { get; }
    public Observable<TouchEndEventArgument> TouchEndEvent { get; }
}

public interface IDoubleTapView
{
    public Observable<Unit> DoubleTapEvent { get; }
}

public readonly struct TouchStartEventArgument
{
    public Vector2 TouchPosition { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TouchStartEventArgument(Vector2 touchPosition)
    {
        TouchPosition = touchPosition;
    }
}

public readonly struct FingerDraggingInfo
{
    public Vector2 TouchStartPosition { get; }
    public Vector2 CurrentPosition { get; }
    public Vector2 Delta => CurrentPosition - TouchStartPosition;
    public Vector2 CurrentFrameDelta { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public FingerDraggingInfo
    (
        Vector2 touchStartPosition,
        Vector2 currentPosition,
        Vector2 currentFrameDelta
    )
    {
        TouchStartPosition = touchStartPosition;
        CurrentPosition = currentPosition;
        CurrentFrameDelta = currentFrameDelta;
    }
}

public readonly struct TouchEndEventArgument
{
    private Vector2 TouchStartPosition { get; }
    private Vector2 TouchEndPosition { get; }
    public Vector2 Delta => TouchStartPosition - TouchEndPosition;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TouchEndEventArgument(Vector2 touchStartPosition, Vector2 touchEndPosition)
    {
        TouchStartPosition = touchStartPosition;
        TouchEndPosition = touchEndPosition;
    }
}