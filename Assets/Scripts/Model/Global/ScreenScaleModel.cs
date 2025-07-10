using Interface.Global.Screen;
using UnityEngine;
using VContainer.Unity;

namespace Model.Global
{
    public class ScreenScaleModel: IScreenScaleModel, ITickable
    {
        public void Tick()
        {
            Width = Screen.width;
            Height = Screen.height;
        }

        public Vector2 Scale => new Vector2(Width, Height);
        public float Width { get; private set; }
        public float Height { get; private set; }
    }
}