using System;
using Domain.IPresenter.Util;

namespace Domain.UseCase.Util
{
    public class ScrollCase: IDisposable
    {
        public ScrollCase
        (
            IScrollPresenter scrollPresenter
        )
        {
            ScrollPresenter = scrollPresenter;

        }

        // private void OnMove(FingerTouchingEventArg touchingEventArg)
        // {
        //     var delta = touchingEventArg.Delta;
        //     ScrollPresenter.AddVelocity(delta);
        // }
        
        private IScrollPresenter ScrollPresenter { get; }

        public void Dispose()
        {
        }
    }
}