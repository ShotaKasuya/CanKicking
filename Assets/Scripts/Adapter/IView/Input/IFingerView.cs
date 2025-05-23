using System;
using Module.Option;
using Structure.Util.Input;

namespace Adapter.IView.Input
{
    public interface IFingerView
    {
        public Action<FingerTouchInfo> OnTouch { get; set; }
        public Option<FingerDraggingInfo> DragInfo { get; }
        public Action<FingerReleaseInfo> OnRelease { get; set; }
    }
}