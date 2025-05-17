using System;
using Domain.IPresenter.OutGame.Title;
using Domain.IPresenter.Scene;
using Structure.Scene;
using VContainer.Unity;

namespace Domain.UseCase.OutGame.Title
{
    public class TitleCase:IStartable, IDisposable
    {
        public TitleCase
        (
            ITitleEventPresenter titleEventPresenter,
            IScenePresenter scenePresenter
        )
        {
            TitleEventPresenter = titleEventPresenter;
            ScenePresenter = scenePresenter;
        }

        public void Start()
        {
            TitleEventPresenter.OnStartGame += OnStart;
        }

        private void OnStart()
        {
            ScenePresenter.Load(SceneType.StageSelect.ToSceneName());
        }

        private ITitleEventPresenter TitleEventPresenter { get; }
        private IScenePresenter ScenePresenter { get; }

        public void Dispose()
        {
            TitleEventPresenter.OnStartGame -= OnStart;
        }
    }
}