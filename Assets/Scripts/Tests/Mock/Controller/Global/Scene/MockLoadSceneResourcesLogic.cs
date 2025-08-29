using Cysharp.Threading.Tasks;
using Interface.Logic.Global;

namespace Tests.Mock.Controller.Global.Scene
{
    public class MockLoadSceneResourcesLogic : ILoadSceneResourcesLogic
    {
        public bool IsLoadCalled { get; private set; }
        public bool IsUnloadCalled { get; private set; }

        public UniTask LoadResources()
        {
            IsLoadCalled = true;
            return UniTask.CompletedTask;
        }

        public UniTask UnLoadResources()
        {
            IsUnloadCalled = true;
            return UniTask.CompletedTask;
        }
    }
}
