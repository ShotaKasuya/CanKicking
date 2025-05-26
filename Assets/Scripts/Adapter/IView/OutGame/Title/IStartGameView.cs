using R3;

namespace Adapter.IView.OutGame.Title
{
    public interface IStartGameView
    {
        public Observable<Unit> StartEvent { get; }
    }
}