using System;
using Adapter.IView.OutGame.StageSelect;
using Domain.IPresenter.OutGame.StageSelect;
using Module.Option;

namespace Adapter.Presenter.OutGame.StageSelect
{
    public class PlayerStageSelectionPresenter : IPlayerStageSelectionPresenter, IDisposable
    {
        public PlayerStageSelectionPresenter
        (
            ISelectedStageView selectedStageView
        )
        {
            SelectedStageView = selectedStageView;

            SelectedStageView.StageSelectEvent += OnSelect;
        }

        public Action<Option<string>> SelectEvent { get; set; }

        private void OnSelect(Option<string> selection)
        {
            SelectEvent.Invoke(selection);
        }
        
        private ISelectedStageView SelectedStageView { get; }

        public void Dispose()
        {
            SelectedStageView.StageSelectEvent -= OnSelect;
        }
    }
}