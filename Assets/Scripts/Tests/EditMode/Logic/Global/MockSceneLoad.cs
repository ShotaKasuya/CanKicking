using Cysharp.Threading.Tasks;
using Interface.Logic.Global;

namespace Tests.EditMode.Logic.Global
{
    public class MockLoadPrimarySceneLogic : ILoadPrimarySceneLogic
    {
        public string CalledScenePath { get; private set; }

        public UniTask ChangeScene(string scenePath)
        {
            CalledScenePath = scenePath;
            return UniTask.CompletedTask;
        }
    }
}