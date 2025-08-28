using R3;

namespace Interface.View.OutGame
{
    /// <summary>
    /// ゲームスタートのイベントを発行する
    /// </summary>
    public interface IStartGameView
    {
        public Observable<Unit> StartEvent { get; }
    }
}