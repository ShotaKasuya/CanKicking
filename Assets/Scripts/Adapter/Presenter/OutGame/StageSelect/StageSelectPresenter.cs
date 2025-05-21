using Adapter.IView.OutGame.StageSelect;
using Domain.IPresenter.OutGame.StageSelect;

namespace Adapter.Presenter.OutGame.StageSelect
{
    public class StageSelectPresenter: IStageSelectPresenter
    {
        public StageSelectPresenter
        (
            ISelectedStageTextView selectedStageTextView
        )
        {
            SelectedStageTextView = selectedStageTextView;
        }
        
        public void PresentSelectedStage(string selectedStage)
        {
            SelectedStageTextView.SetStage(selectedStage);
        }

        public void PresentCancelSelection()
        {
            SelectedStageTextView.ResetStage();
        }
        
        private ISelectedStageTextView SelectedStageTextView { get; }
    }
}