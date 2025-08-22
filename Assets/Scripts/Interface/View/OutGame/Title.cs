using R3;

namespace Interface.OutGame.Title
{
    /// <summary>
    /// ゲームスタートのイベントを発行する
    /// </summary>
    public interface IStartGameView
    {
        public Observable<Unit> StartEvent { get; }
    }
}