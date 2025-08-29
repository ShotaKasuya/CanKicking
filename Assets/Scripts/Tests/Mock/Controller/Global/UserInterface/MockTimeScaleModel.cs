using Structure.Global.TimeScale;
using Interface.Model.Global;

namespace Tests.Mock.Controller.Global.UserInterface
{
    public class MockTimeScaleModel : ITimeScaleModel
    {
        public bool IsResetCalled { get; private set; }
        public void Execute(TimeCommandType timeCommand) { }
        public void Undo() { }
        public void Reset() => IsResetCalled = true;
    }
}
