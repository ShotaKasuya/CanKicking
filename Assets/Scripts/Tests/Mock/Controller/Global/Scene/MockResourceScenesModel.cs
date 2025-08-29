using System.Collections.Generic;
using Interface.Model.Global;
using Module.SceneReference.Runtime;

namespace Tests.Mock.Controller.Global.Scene
{
    public class MockResourceScenesModel : IResourceScenesModel
    {
        public IReadOnlyList<string> GetResourceScenes() => new List<string>();

        public void PushReleaseContext(SceneContext sceneContext)
        {
        }

        public IReadOnlyList<SceneContext> GetSceneReleaseContexts() => new List<SceneContext>();
    }
}
