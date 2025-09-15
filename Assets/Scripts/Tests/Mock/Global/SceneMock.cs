using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.Global;
using Module.SceneReference.Runtime;
using R3;

namespace Tests.Mock.Global
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

    public class MockResourceScenesModel : IResourceScenesModel
    {
        public IReadOnlyList<string> GetResourceScenes() => new List<string>();

        public void PushReleaseContext(SceneContext sceneContext)
        {
        }

        public IReadOnlyList<SceneContext> GetSceneReleaseContexts() => new List<SceneContext>();
    }

    public class MockSceneLoadEventModel : ISceneLoadEventModel
    {
        private readonly Subject<Unit> _startLoadScene = new();
        private readonly Subject<Unit> _afterSceneUnload = new();
        private readonly Subject<Unit> _beforeSceneLoad = new();
        private readonly Subject<Unit> _endLoadScene = new();

        public Observable<Unit> StartLoadScene => _startLoadScene;
        public Observable<Unit> AfterSceneUnLoad => _afterSceneUnload;
        public Observable<Unit> BeforeSceneLoad => _beforeSceneLoad;
        public Observable<Unit> AfterSceneLoad => Observable.Empty<Unit>();
        public Observable<Unit> BeforeNextSceneActivate => Observable.Empty<Unit>();
        public Observable<Unit> AfterNextSceneActivate => Observable.Empty<Unit>();
        public Observable<Unit> BeforeSceneUnLoad => Observable.Empty<Unit>();
        public Observable<Unit> EndLoadScene => _endLoadScene;

        public void SimulateAfterSceneUnload() => _afterSceneUnload.OnNext(Unit.Default);
        public void SimulateBeforeSceneLoad() => _beforeSceneLoad.OnNext(Unit.Default);
        public void SimulateStartLoadScene() => _startLoadScene.OnNext(Unit.Default);
        public void SimulateEndLoadScene() => _endLoadScene.OnNext(Unit.Default);
    }
}