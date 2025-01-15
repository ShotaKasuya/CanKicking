using DataUtil.Util.Input;

namespace Adapter.IView.Finger
{
    public interface IFingerView
    {
        public TouchState TouchState { get; }
    }
}