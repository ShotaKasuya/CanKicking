using System;

namespace Adapter.IView.InGame.UI
{
    public interface ISceneChangeEventView
    {
        public Action<string> SceneChangeEvent { get; set; }
    }
}