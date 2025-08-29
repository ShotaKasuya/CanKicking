using Cysharp.Threading.Tasks;
using Interface.View.Global;

namespace Tests.Mock.Controller.Global.UserInterface
{
    public class MockLoadingPanelView : ILoadingPanelView
    {
        public bool IsShowPanelCalled { get; private set; }
        public bool IsHidePanelCalled { get; private set; }
        public UniTask ShowPanel() { IsShowPanelCalled = true; return UniTask.CompletedTask; }
        public UniTask HidePanel() { IsHidePanelCalled = true; return UniTask.CompletedTask; }
    }
}
