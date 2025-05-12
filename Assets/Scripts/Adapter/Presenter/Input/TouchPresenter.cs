using System;
using Adapter.IView.Finger;
using Domain.IPresenter.Util;
using Module.Option;
using Structure.Util.Input;

namespace Adapter.Presenter.Input
{
    public class TouchPresenter : ITouchPresenter, IDragFingerPresenter, IFingerReleasePresenter, IDisposable
    {
        public TouchPresenter(IFingerView fingerView)
        {
            FingerView = fingerView;
            FingerView.OnTouch += InvokeOnTouch;
            FingerView.OnRelease += InvokeOnRelease;
        }

        private void InvokeOnTouch(FingerTouchInfo fingerTouchInfo)
        {
            OnTouch?.Invoke(fingerTouchInfo);
        }

        private void InvokeOnRelease(FingerReleaseInfo fingerReleaseInfo)
        {
            OnRelease?.Invoke(fingerReleaseInfo);
        }

        public Action<FingerTouchInfo> OnTouch { get; set; }
        public Option<FingerDraggingInfo> DragInfo => FingerView.DragInfo;
        public Action<FingerReleaseInfo> OnRelease { get; set; }

        private IFingerView FingerView { get; }

        public void Dispose()
        {
            FingerView.OnTouch -= InvokeOnTouch;
            FingerView.OnRelease -= InvokeOnRelease;
        }
    }
}