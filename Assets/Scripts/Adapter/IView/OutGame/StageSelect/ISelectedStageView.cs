using System;
using Module.Option;

namespace Adapter.IView.OutGame.StageSelect
{
    public interface ISelectedStageView
    {
        public Action<Option<string>> StageSelectEvent { get; set; }
    }

    public interface ISelectedStageTextView
    {
        public void SetStage(string selectedStage);
        public void ResetStage();
    }
}