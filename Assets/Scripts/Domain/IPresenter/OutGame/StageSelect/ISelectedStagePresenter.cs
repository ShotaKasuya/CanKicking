using System;

namespace Domain.IPresenter.OutGame.StageSelect
{
    public interface ISelectedStagePresenter
    {
        public Action<string> SelectEvent { get; set; }
    }
}