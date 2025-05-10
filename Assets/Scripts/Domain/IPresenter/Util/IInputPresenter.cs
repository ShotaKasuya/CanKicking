using System;
using Module.Option;
using Structure.Util.Input;

namespace Domain.IPresenter.Util
{
    public interface ITouchPresenter
    {
        public Action<FingerTouchInfo> OnTouch { get; set; }
    }

    public interface IDragFingerPresenter
    {
        public Option<FingerDraggingInfo> DragInfo { get; }
    }

    public interface IFingerReleasePresenter
    {
        public Action<FingerReleaseInfo> OnRelease { get; set; }
    }
}