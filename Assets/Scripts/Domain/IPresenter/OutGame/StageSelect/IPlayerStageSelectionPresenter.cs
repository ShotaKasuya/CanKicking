using System;
using Module.Option;

namespace Domain.IPresenter.OutGame.StageSelect
{
    public interface IPlayerStageSelectionPresenter
    {
        public Action<Option<string>> SelectEvent { get; set; }
    }

    public interface IStageSelectPresenter
    {
        public void PresentSelectedStage(string selectedStage);
        public void PresentCancelSelection();
    }
}