using System;
using Adapter.IView.OutGame.Title;
using Domain.IPresenter.OutGame.Title;

namespace Adapter.Presenter.OutGame.Title
{
    public class TitleEventPresenter : ITitleEventPresenter, IDisposable
    {
        public TitleEventPresenter
        (
            IStartGameView startGameView)
        {
            StartGameView = startGameView;

            startGameView.StartEvent += OnStart;
        }

        private void OnStart()
        {
            OnStartGame.Invoke();
        }

        public Action OnStartGame { get; set; }
        
        private IStartGameView StartGameView { get; }

        public void Dispose()
        {
            StartGameView.StartEvent -= OnStartGame;
        }
    }
}