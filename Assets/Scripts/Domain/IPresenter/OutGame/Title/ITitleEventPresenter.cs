using System;

namespace Domain.IPresenter.OutGame.Title
{
    public interface ITitleEventPresenter
    {
        public Action OnStartGame { get; set; }
    }
}