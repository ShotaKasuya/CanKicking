using System;

namespace Domain.IPresenter.InGame.UI
{
    public interface ISceneChangePresenter
    {
        public Action<string> SceneChangeEvent { get; set; }
    }
}