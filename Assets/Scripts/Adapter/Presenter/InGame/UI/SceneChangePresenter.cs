using System;
using System.Collections.Generic;
using Adapter.IView.InGame.UI;
using Domain.IPresenter.InGame.UI;

namespace Adapter.Presenter.InGame.UI
{
    public class SceneChangePresenter: ISceneChangePresenter, IDisposable
    {
        public SceneChangePresenter(IReadOnlyList<ISceneChangeEventView> sceneChangeEventViews)
        {
            SceneChangeEventViews = sceneChangeEventViews;
            foreach (var sceneChangeEventView in sceneChangeEventViews)
            {
                sceneChangeEventView.SceneChangeEvent += InvokeSceneChangeEvent;
            }
        }
        
        public Action<string> SceneChangeEvent { get; set; }

        private void InvokeSceneChangeEvent(string sceneName)
        {
            SceneChangeEvent.Invoke(sceneName);
        }
        
        private IReadOnlyList<ISceneChangeEventView> SceneChangeEventViews { get; }

        public void Dispose()
        {
            foreach (var sceneChangeEventView in SceneChangeEventViews)
            {
                sceneChangeEventView.SceneChangeEvent -= InvokeSceneChangeEvent;
            }
        }
    }
}