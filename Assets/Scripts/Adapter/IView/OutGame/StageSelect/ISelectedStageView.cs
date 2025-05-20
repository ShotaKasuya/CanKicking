using System;
using Module.Option;

namespace Adapter.IView.OutGame.StageSelect
{
    public interface ISelectedStageView
    {
        public Action<Option<string>> SelectStageEvent { get; set; }
    }
}