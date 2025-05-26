using Module.Option;
using R3;

namespace Adapter.IView.OutGame.StageSelect
{
    public interface ISelectedStageView
    {
        public Observable<Option<string>> SelectEvent { get; set; }
    }

    public interface ISelectedStageTextView
    {
        public void SetStage(string selectedStage);
        public void ResetStage();
    }
}