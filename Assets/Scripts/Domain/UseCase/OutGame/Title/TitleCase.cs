using System;
using DataUtil.Scene;
using Domain.IPresenter.OutGame.Title;
using Domain.IPresenter.Scene;

namespace Domain.UseCase.OutGame.Title
{
    public class TitleCase: IDisposable
    {
        public TitleCase
        (
            ITitleEventPresenter titleEventPresenter,
            IScenePresenter scenePresenter
        )
        {
            TitleEventPresenter = titleEventPresenter;
            ScenePresenter = scenePresenter;

            titleEventPresenter.OnStartGame += OnStart;
        }

        private void OnStart()
        {
            ScenePresenter.Load(SceneType.StageSelect);
        }

        private ITitleEventPresenter TitleEventPresenter { get; }
        private IScenePresenter ScenePresenter { get; }

        public void Dispose()
        {
            TitleEventPresenter.OnStartGame -= OnStart;
        }
    }
}