using System;
using Domain.IPresenter.Util;
using Domain.IPresenter.Util.Input;

namespace Domain.UseCase.Util
{
    public class ScrollCase: IDisposable
    {
        public ScrollCase
        (
            IFingerTouchingEventPresenter touchingEventPresenter,
            IScrollPresenter scrollPresenter
        )
        {
            TouchingEventPresenter = touchingEventPresenter;
            ScrollPresenter = scrollPresenter;

            touchingEventPresenter.TouchingEvent += OnMove;
        }

        private void OnMove(FingerTouchingEventArg touchingEventArg)
        {
            var delta = touchingEventArg.Delta;
            ScrollPresenter.AddVelocity(delta);
        }
        
        private IFingerTouchingEventPresenter TouchingEventPresenter { get; }
        private IScrollPresenter ScrollPresenter { get; }

        public void Dispose()
        {
            TouchingEventPresenter.TouchingEvent -= OnMove;
        }
    }
}