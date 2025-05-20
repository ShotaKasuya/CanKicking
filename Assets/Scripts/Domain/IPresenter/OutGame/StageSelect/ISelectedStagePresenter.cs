using System;
using Module.Option;

namespace Domain.IPresenter.OutGame.StageSelect
{
    public interface ISelectedStagePresenter
    {
        public Action<Option<string>> SelectEvent { get; set; }
    }
}