using System;

namespace Adapter.IView.OutGame.Title
{
    public interface IStartGameView
    {
        public Action StartEvent { get; set; }
    }
}