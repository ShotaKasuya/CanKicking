using System;
using DataUtil.Util.Input;

namespace Domain.IPresenter.Util.Input
{
    public interface IFingerReleaseEventPresenter
    {
        Action<FingerReleaseEventArg> ReleaseEvent { get; set; }
    }
}