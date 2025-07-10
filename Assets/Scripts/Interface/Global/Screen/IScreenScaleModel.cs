using UnityEngine;

namespace Interface.Global.Screen;

public interface IScreenScaleModel
{
    public Vector2 Scale { get; }
    public float Width { get; }
    public float Height { get; }
}
