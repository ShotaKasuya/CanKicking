using System;

namespace Domain.IPresenter.InGame.UI
{
    public interface IStopEventPresenter
    {
        public Action StopEvent { get; set; }
    }

    /// <summary>
    /// 停止から再生へ移行するイベント
    /// </summary>
    public interface IPlayEventPresenter
    {
        public Action PlayEvent { get; set; }
    }
}